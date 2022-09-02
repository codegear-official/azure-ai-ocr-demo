using System;
using System.IO;
using System.Diagnostics;   // for Debug.WriteLine()
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;    // for MessageBox()

using Windows.Storage;  // for FileAccessMode
using Windows.Storage.Streams;  // for IRandomAccessStream
using Windows.Media.Capture;
using Windows.Foundation;   // for Size()
using Windows.Graphics.Imaging;

// Import namespaces
// requires Microsoft.Azure.CognitiveServices.Vision.ComputerVision, 6.0.0 NuGet module
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;   // for ReadOperationResult

namespace UWPDemo1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // full path for Picture file
        private static string picture_path = null;

        public MainPage()
        {
            this.InitializeComponent();

            // Copy template Configuration file if not exist under AppData\Local\Packages folder
            // Note: Before requesting OCR, the user must set the appropriate TOKEN/ENDPOINT values.
            _ = CopyConfigFile();
        }

        public bool Is_ProcessOCR_Requested()
        {
            return IsOCREnabled.IsChecked;  // get "ToggleMenuFlyoutItem.IsChecked" property
        }

        private async void OnSelectPicture(object sender, RoutedEventArgs e)
        {
            MyMessage.Text = "Select Picture....";

            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            // get 
            StorageFile file = await picker.PickSingleFileAsync();

            // 'file' is null if user cancels the file picker.
            if (file == null)
            {
                // User cancelled photo capture
                MyMessage.Text = "Operation cancelled.";
                return;
            }

            // Open a stream for the selected file.
            // The 'using' block ensures the stream is disposed
            // after the image is loaded.
            using (IRandomAccessStream fileStream =
                await file.OpenAsync(FileAccessMode.Read))
            {
                // Set the image source to the selected bitmap.
                Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
                    new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                bitmapImage.SetSource(fileStream);
                MyPicture.Source = bitmapImage;
            }
            // close it?

            if (!IsOCREnabled.IsChecked)
            {
                // user requested not to process OCR
                MyMessage.Text = "Select Picture....OK";
                return;
            }

            // Open a stream to send selected picture to AI-OCR
            ReadOperationResult results = null;
            using (var fileStream2 = await file.OpenReadAsync())
            {
                // set fullpath of display file
                picture_path = file.Path;
                Debug.WriteLine($"picture_path set to {picture_path}");

                // convert WinRT Stream to .NET stream using AsStream()
                // see https://docs.microsoft.com/ja-jp/dotnet/standard/io/how-to-convert-between-dotnet-streams-and-winrt-streams for details.
                results = await GetTextRead(fileStream2.AsStream());
            }
            // close it?

            DebugResult(results);   // Debug Output

            if (results == null)
            {
                // may be no "appsetting.json" file exist or invalid API Key/Endpoint
                MyMessage.Text = "Start Analyze... FAILED : check configuration file!";

                // FORCE reload "appsetting.json" file next time.
                cvClient = null;
                return;
            }

            // otherwise, result returned
            // If the operation was successfuly, process the text line by line
            if (results.Status == OperationStatusCodes.Succeeded)
            {
                // set output to Text control
                string result_text = DecodeResult(results);
                if (result_text != null)
                {
                    // set TextBox control
                    MyText.Text = result_text;
                    MyMessage.Text = "Start Analyze... SUCCESS!";
                }
                else
                {
                    MyMessage.Text = "DecodeResult Error!";
                }

                // anyway...
                return;
            }
            else
            {
                // otherwise...returned ERROR status value
                MyMessage.Text = $"Start Analyze... FAILED : {results.Status}";
            }
        }

        private async void OnTakePicture(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            //captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);
            captureUI.PhotoSettings.AllowCropping = false;
            captureUI.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.VeryLarge5M;  //　5M Pixels    

            MyMessage.Text = "Take Picture....";

            try
            {
                StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

                if (photo == null)
                {
                    // User cancelled photo capture
                    MyMessage.Text = "Operation cancelled.";
                    return;
                }

                // copy temporal capture file to "Pictures" Folder
                // this requires adding capability in "Package.appxmanifest" file
                // See https://docs.microsoft.com/ja-jp/uwp/schemas/appxpackage/uapmanifestschema/element-uap-capability for details

                //StorageFile copy = await photo.CopyAsync(KnownFolders.PicturesLibrary, "UWPDemo.jpg", NameCollisionOption.ReplaceExisting);
                StorageFile copy = await photo.CopyAsync(KnownFolders.PicturesLibrary, "UWPDemo.jpg", NameCollisionOption.GenerateUniqueName);
                await photo.DeleteAsync();
                photo = null;   // for safety

                // start access copied file 
                IRandomAccessStream stream = await copy.OpenAsync(FileAccessMode.Read);

                // The 'using' block ensures the stream is disposed
                // after the image is loaded.
                using (IRandomAccessStream fileStream =
                    await copy.OpenAsync(FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
                        new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                    bitmapImage.SetSource(fileStream);
                    MyPicture.Source = bitmapImage;

                    // set fullpath of display file
                    picture_path = copy.Path;
                    Debug.WriteLine($"picture_path set to {picture_path}");
                }

                // photo capture SUCCESS
                MyMessage.Text = "Take Picture SUCCESS.";

                if (!IsOCREnabled.IsChecked)
                {
                    // user requested not to process OCR
                    MyMessage.Text = "Select Picture....OK";
                    return;
                }

                // Open a stream to send selected picture to AI-OCR
                ReadOperationResult results = null;
                using (var fileStream2 = await copy.OpenReadAsync())
                {
                    // set fullpath of display file
                    picture_path = copy.Path;
                    Debug.WriteLine($"picture_path set to {picture_path}");

                    // convert WinRT Stream to .NET stream using AsStream()
                    // see https://docs.microsoft.com/ja-jp/dotnet/standard/io/how-to-convert-between-dotnet-streams-and-winrt-streams for details.
                    results = await GetTextRead(fileStream2.AsStream());
                }
                // close it?

                DebugResult(results);   // Debug Output


                if (results == null)
                {
                    // may be no "appsetting.json" file exist or invalid API Key/Endpoint
                    MyMessage.Text = "Start Analyze... FAILED : check configuration file!";

                    // FORCE reload "appsetting.json" file next time.
                    cvClient = null;
                    return;
                }

                // otherwise, result returned
                // If the operation was successfuly, process the text line by line
                if (results.Status == OperationStatusCodes.Succeeded)
                {
                    // set output to Text control
                    string result_text = DecodeResult(results);
                    if (result_text != null)
                    {
                        // set TextBox control
                        MyText.Text = result_text;
                        MyMessage.Text = "Start Analyze... SUCCESS!";
                    }
                    else
                    {
                        MyMessage.Text = "DecodeResult Error!";
                    }

                    // anyway...
                    return;
                }
                else
                {
                    // otherwise...returned ERROR status value
                    MyMessage.Text = $"Start Analyze... FAILED : {results.Status}";
                }

            }
            catch (Exception ex)
            {
                // User cancelled photo capture
                MyMessage.Text = $"Exception in Capture: ({ex.Message})";
                return;
            }
        }

        private /*async*/ void OnApplyOCR(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"OnApplyOCR: Apply OCR " + (IsOCREnabled.IsChecked ? "Checked." : "Unchecked."));
        }

        private async void OnOpenConfig(object sender, RoutedEventArgs e)
        {
            try
            {
                bool result = await Windows.System.Launcher.LaunchFolderAsync(appdata_dir);
                Debug.WriteLine($"OnOpenConfig : LaunchFolderAsync returned {result}");
            }
            catch (Exception ex)
            {
                // other exception such as UnauthorizedAccessException
                Debug.WriteLine($"OnOpenConfig : LaunchFolderAsync Exception {ex.Message}");
            }
        }

        private async void OnAbout(object sender, RoutedEventArgs e)
        {
            ContentDialog dlg = new ContentDialog()
            {
                Title = "AI-OCR Cognitive Demo",
                Content = "Copyright 2022 Codegear, Inc",
                CloseButtonText = "Ok"
            };

            await dlg.ShowAsync();
        }

    }
}
