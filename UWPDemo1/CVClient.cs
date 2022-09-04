//
// Implement Conputer vision clinet in partial class (supplements MainPage.xaml.cs)
//
using System;
using System.IO;
using System.Diagnostics;   // for Debug.WriteLine()
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;  // for StorageFolder class

using Microsoft.Extensions.Configuration;   // requires Microsoft.Extensions.Configuration.Json NuGet module

// Import namespaces
// requires Microsoft.Azure.CognitiveServices.Vision.ComputerVision, 6.0.0 NuGet module
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace UWPDemo1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // App package installed location i.e. "ms-appx:///" or just "/"
        public static StorageFolder installed_dir = Windows.ApplicationModel.Package.Current.InstalledLocation;

        // Appdata\Local directory i.e. "ms-appdata:///local/"
        public static StorageFolder appdata_dir = Windows.Storage.ApplicationData.Current.LocalFolder;

        // filename of config file
        public static string config_file = "appsettings.json";


        // ConputerVision client object
        private static ComputerVisionClient cvClient = null;

        // Copy template appsettings.json to AppData\Local\Packages folder (if not exist)
        // return true if already exist or copy success
        // Note: Parameters in appsettings.json may be invalid
        public static async Task<bool> CopyConfigFile()
        {
            // check if "appsetting.json" exist in AppData\local
            StorageFile dest = null;
            try
            {
                dest = await appdata_dir.GetFileAsync(config_file);
            }
            catch (FileNotFoundException ex)
            {
                // FileNotFoundException
                Debug.WriteLine($"InitSettingFile : Exception {ex.Message}");
                // copy file later this function
            }
            catch (Exception ex)
            {
                // other exception such as UnauthorizedAccessException
                Debug.WriteLine($"InitSettingFile : Exception {ex.Message}");
                return false;
            }

            if (dest != null)
            {
                // open success
                Debug.WriteLine($"InitSettingFile : Config File '{dest.Path}' Exist.");

                // return SUCCESS = file exist.
                return true;
            }

            // otherwise, Config File not exist
            // now open src file and copy
            StorageFile src = null;
            try
            {
                src = await installed_dir.GetFileAsync(config_file);
            }
            catch (Exception ex)
            {
                // exception such as FileNotFoundException / UnauthorizedAccessException etc.
                Debug.WriteLine($"InitSettingFile : Source file Exception {ex.Message}");
                return false;
            }

            try
            {
                dest = await src.CopyAsync(appdata_dir, config_file, NameCollisionOption.ReplaceExisting);    // overwrite it
            }
            catch (Exception ex)
            {
                // exception such as UnauthorizedAccessException etc.
                Debug.WriteLine($"InitSettingFile : CopyAsync Exception {ex.Message}");
                return false;
            }

            // otherwise, copy success!
            Debug.WriteLine($"InitSettingFile : Copy to '{dest.Path}'  SUCCESS.");

            return true;
        }

        // read appsetting.json and construct ConputerVision client object
        // we can call this method multiple times while the client object creation fails.
        public static bool InitComputerVision()
        {
            if (cvClient == null)
            {
                // (re)try started...

                // try loading API Key and Endpoint
                try
                {
                    // Get config settings from AppData\Local\appsettings.json
                    IConfigurationBuilder builder = new ConfigurationBuilder()
                        .SetBasePath(appdata_dir.Path)
                        .AddJsonFile(config_file);
                    IConfigurationRoot configuration = builder.Build();
                    string cogSvcEndpoint = configuration["CognitiveServicesEndpoint"];
                    string cogSvcKey = configuration["CognitiveServiceKey"];

                    // Authenticate Computer Vision client
                    ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(cogSvcKey);
                    cvClient = new ComputerVisionClient(credentials)
                    {
                        // set construct parameter (cvClient.Endpoint)
                        Endpoint = cogSvcEndpoint
                    };

                    // appsetting.json read SUCCESS but the parameters may be still invalid
                    Debug.WriteLine("InitComputerVision: read setting file SUCCESS!");

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"InitComputerVision Exception: {ex.Message}");
                }

                // anywayy, we tried
            }

            return cvClient != null;    // true if SUCCESS
        }

        // process ConputerVision, returns ReadOperationResult object if success, or null if failed
        public static async Task<ReadOperationResult> GetTextRead(System.IO.Stream imageData)
        {
            // construct ConputerVision client object, if not constructed before
            if (!InitComputerVision())
            {
                // GetTextRead() failed.
                return null;
            }

            //Debug.WriteLine($"Reading text in {imageFile}\n");

            try
            {
                // send picture data to Azure OCR, may raise exception if parameter wrong etc...
                var readOp = await cvClient.ReadInStreamAsync(imageData);

                // now response received
                // Get the async operation ID so we can check for the results
                string operationLocation = readOp.OperationLocation;
                string operationId = operationLocation.Substring(operationLocation.Length - 36);

                // Wait for the asynchronous operation to complete
                ReadOperationResult results;
                do
                {
                    Thread.Sleep(1000);
                    results = await cvClient.GetReadResultAsync(Guid.Parse(operationId));
                }
                while ((results.Status == OperationStatusCodes.Running ||
                        results.Status == OperationStatusCodes.NotStarted));

                return results;
            }
            catch (Exception ex)
            {
                // if appsetting.json is invalid, may detect "Invalid URI: The format of the URI could not be determined" exception
                Debug.WriteLine($"GetTextRead Exception: {ex.Message}");
            }

            // GetTextRead() failed.
            return null;
        }


        // helper function to decode result into string (or null if no result)
        public static string DecodeResult(ReadOperationResult results)
        {
            string buffer = "";

            if (results == null)
            {
                Debug.WriteLine("No Result Returned.");
                return null;
            }

            // If the operation was successfuly, process the text line by line
            if (results.Status != OperationStatusCodes.Succeeded)
            {
                Debug.WriteLine($"Result is {results.Status}.");
                return null;
            }

            // otherwise, results.Status == OperationStatusCodes.Succeeded
            var textUrlFileResults = results.AnalyzeResult.ReadResults;
            foreach (ReadResult page in textUrlFileResults)
            {
                foreach (Line line in page.Lines)
                {
                    buffer += (line.Text + Environment.NewLine);
                }
            }

            return buffer;
        }

        // helper function to Debug Output
        public static void DebugResult(ReadOperationResult results)
        {
            if (results == null)
            {
                Debug.WriteLine("No Result Returned.");
                return;
            }

            // If the operation was successfuly, process the text line by line
            if (results.Status != OperationStatusCodes.Succeeded)
            {
                Debug.WriteLine($"Result is {results.Status}.");
                return;
            }

            // otherwise, results.Status == OperationStatusCodes.Succeeded
            var textUrlFileResults = results.AnalyzeResult.ReadResults;
            foreach (ReadResult page in textUrlFileResults)
            {
                foreach (Line line in page.Lines)
                {
                    //byte[] data = System.Text.Encoding.Unicode.GetBytes(line.Text);

                    Debug.WriteLine(line.Text);
                    //Debug.WriteLine(BitConverter.ToString(data));

                }
            }
        }
    }
}

