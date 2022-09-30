# azure-ai-ocr-demo
Windows UWP project for demonstrating Microsoft Azure Cognitive Read API

このプロジェクトはMicrosoft Azure の Cognitive Services のうち、Computer Vision OCR を実行するGUIデモプログラムです。

C# 記述のUWPプロジェクトで、.NET Native により x86/x64/ARM64 の .msixbundle ファイルを生成します。


# 開発環境

Windows 10 version 21H2 (64bit)をインストールした開発PC上に Visual Studio 2019 version 16.11 をインストールして開発しました。

もしVisual Studio 2019 Comunity Editionを利用したい場合は、現在通常の [Visual Studio Community のページ](https://visualstudio.microsoft.com/ja/vs/community/)からはダウンロードできなくなっています。
無料のVisual Studio Dev Essentials プログラムに加入すると、Visual Studio Subscriptionサイトからダウンロード可能なので試してみてください。

Visual Studio Dev Essentials
https://visualstudio.microsoft.com/ja/dev-essentials/

ダウンロードページ (要ログイン)
https://my.visualstudio.com/Downloads?q=Visual%20Studio%202019

Visual Studio Installer では ワークロードとして「ユニバーサルWindowsプラットフォーム開発」「.NETデスクトップ開発」を選択します。
また、個別のコンポーネントとして「Git for Windows」を選んでインストールしておくと便利です。

このドキュメントではメニューなどのUI項目を英語で表示しています。またMS Learnのページなど、未翻訳のページでは英語版UIで説明されます。
もし英語表示を行う場合には「言語パック」で「英語」を追加してインストールした後、Visual Studio IDEの Toolsメニューの Options で国別設定を表示し、Englishを選んでIDEを再起動すると英語UIになります。

# Azure ポータルでリソースを作成し、ENDPOINT情報とKEY情報を取得する

以下を参考に[Azure ポータル](https://portal.azure.com/#create/Microsoft.CognitiveServicesComputerVision)でAzure Cognitive Servicesのリソースを作成します。

https://docs.microsoft.com/ja-jp/azure/cognitive-services/computer-vision/quickstarts-sdk/client-library?tabs=visual-studio&pivots=programming-language-csharp#prerequisites

リソースを作成したら、「キーとエンドポイント」で表示される項目のうち

- キー1 または キー2 (どちらでもよい)
- エンドポイント

の2つの情報をメモしておきます。

詳しい方法は以下のトレーニング演習をやってみることでも理解できます。

[Computer Vision サービスを使用して画像やドキュメント内のテキストを読み取る](https://docs.microsoft.com/ja-jp/learn/modules/read-text-images-documents-with-computer-vision-service/)


# GitHubからソースコードプロジェクトを clone して開く

Visual Studio 2019 を起動後、「clone a repository」を選んで https://github.com/codegear-official/azure-ai-ocr-demo.git を入力すると ローカルにcloneした後プロジェクトが開きます。

https://github.com/codegear-official/azure-ai-ocr-demo.git

ビルド Configurationの選択で 「Debug」「x64」を選んで Build メニューから「build UWPDemo1」を実行するとビルド開始します。しばらくしてビルド成功すると、Output ウインドウのBuildで

    ========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========

のように表示され、ビルドが成功します。

# 開発PCでデバッグ実行

Debug メニューから「Start Debugging」を選ぶか、F5キーを押すとデバッグ実行を開始します。初回はデバッグシンボルをダウンロードするので、起動に時間がかかります (スキップも可能)。

起動したらまず 「Setting」メニューから「Open Config」を選んで設定ファイル appsettings.json のあるディレクトリを開きます。

当初、以下の内容の Jsonファイルが入っているので、実際に利用可能な ENDPOINTとKEY情報に書き換えます。

    {
        "CognitiveServicesEndpoint": "YOUR_COGNITIVE_SERVICES_ENDPOINT",
        "CognitiveServiceKey": "YOUR_COGNITIVE_SERVICES_KEY"
    }
    
**変更後の例**

    {
        "CognitiveServicesEndpoint": "https://XXXXXXXX.cognitiveservices.azure.com/",
        "CognitiveServiceKey": "2c62dea8fc12489f8300f5bcbbe0e493"
    }

ファイルを書き換えたらExplorerウインドウをクローズします。

この後、「Pick」を選んで OCR処理を行う画像ファイルを選択するか。「Take Picture」を選んで、カメラデバイスからOCRを実行する画像をキャプチャします。

もしOCR処理が成功したら、コマンドバー上に SUCCESS の文字が表示されます。
画像をスクロールして下の部分を表示すると、認識された文字列が表示されていることがわかります。

ウインドウ右上のクローズボタン (×印) をクリックすると、デモアプリを終了します。

# 他のPCで実行するためにパッケージをビルド

もし動作に問題なければ、Projectメニューの「Publish」から「Create App Packages...」を選んでパッケージをビルドします。

このダイアログを利用して Microsoft Storeに提出するためのパッケージを作成したり、サイドローディング用の.msixbundle ファイルなどをビルドすることができます。

サイドローディング用のパッケージをビルドする場合、設定ダイアログで「Sideloading」を選択し Nextを実行。
次の画面で署名用の証明書を設定します。プロジェクト中にはこちらで作成した .pfxファイルが含まれていますが、プロジェクトから削除して新規に.pfxを作成することも可能です。Nextを押します。

次の画面でバージョンおよびパッケージに含めるCPUタイプをチェックして指定します。それぞれのCPUタイプで、なぜか初期状態でDebug ビルドが選択されているので、手動でチェックの入っている項目すべてをRelease ビルドの選択に変更し、Create を押します。しばらくするとパッケージがビルドされ、最終的にビルド結果のファイルが含まれるディレクトリを開くリンクが表示されるので、これをクリックするとExplorerで以下のファイルが含まれるディレクトリが表示されます。

- UWPDemo1_1.0.X.0_x86_x64_arm64.msixbundle : インストール可能なパッケージファイル
- UWPDemo1_1.0.X.0_x86_x64_arm64.cer : 証明書ファイル (いわゆるオレオレ証明書)
- UWPDemo1_1.0.X.0_XXX.appxsym : デバッグシンボルファイル
- Dependencies : 依存モジュールの含まれるディレクトリ

もしビルド済みのバイナリパッケージを参照したい場合は、以下の Releases からダウンロードすることも可能です。

https://github.com/codegear-official/azure-ai-ocr-demo/releases/download/1.0.4.0/UWPDemo1_1.0.4.0_Test.zip

# 別PCにコピーしてインストール

通常の Windowsでは、パッケージのうち「UWPDemo1_1.0.X.0_x86_x64_arm64.msixbundle」「UWPDemo1_1.0.X.0_x86_x64_arm64.cer」の2つのファイルをコピーしておくとサイドローディング用のインストールができます。 (ついでにあらかじめパラメータを設定した appsettings.json もコピーしておくと便利)

1. オレオレ証明書を証明書ストアにインストール

「UWPDemo1_1.0.X.0_x86_x64_arm64.cer」をダブルクリックして開くと、ダイアログが表示され「証明書のインストール...」ボタンが表示されるので、これをクリックしてインストールします。インストール先は必ず「ローカルコンピューター」の「信頼されたルート証明機関」の場所にする必要があります。

インストール後に「コンピューター証明書の管理」管理コンソールアプリを実行し、「信頼されたルート証明機関」の下の「証明書」の一覧の中に発行先と発行者が共に「hiron」になっている証明書が見つかると思います。このテスト用オレオレ証明書は有効期限が1年に設定されているので、証明が有効なのは証明書生成後、最大1年間ということになります。

2. アプリをインストール

「UWPDemo1_1.0.X.0_x86_x64_arm64.msixbundle」をダブルクリックすると、アプリインストーラーが立ちあがります。「インストール」を実行するとインストールされ、さらに「準備が出来たら起動」のチェックが入っていたら、インストール終了後にすぐにアプリが立ち上がります。

# LTSCの場合のインストール方法

「Windows 10 Enterprise LTSC」と「Windows 10 IoT Enterprise LTSC」の場合は、OS側にアプリインストーラー自体が含まれていないため、Explorerで .msixbundle ファイルをダブルクリックしても何も起きずインストール出来ません。

このような場合、**管理者モードの** PowerShellで「Add-AppxPackage」コマンドを利用してインストールすることができます。ただし、この手動インストールの方法では依存関係モジュールも手動で個別にインストールする必要があります。以下の手順でインストールします。

1. オレオレ証明書ファイルのインストール

これは LTSC以外の場合と同じ方法でインストールします。

2. 依存関係モジュールを Dependencies ディレクトリからコピーしてきてインストール

具体的には以下のように実行します。 (以下はARM64の場合)

    PS D:\TEMP\UWPDemo1\Dependencies\arm64> dir

        ディレクトリ: D:\TEMP\UWPDemo1\Dependencies\arm64

    Mode                 LastWriteTime         Length Name
    ----                 -------------         ------ ----
    -a----        2022/08/27      0:28        5172939 Microsoft.NET.Native.Framework.2.2.appx
    -a----        2022/08/27      0:28         248183 Microsoft.NET.Native.Runtime.2.2.appx
    -a----        2022/08/27      0:28        1529007 Microsoft.VCLibs.ARM64.14.00.appx

    PS D:\TEMP\UWPDemo1\Dependencies\arm64> Add-AppxPackage .\Microsoft.VCLibs.ARM64.14.00.appx
    PS D:\TEMP\UWPDemo1\Dependencies\arm64> Add-AppxPackage .\Microsoft.NET.Native.Runtime.2.2.appx
    PS D:\TEMP\UWPDemo1\Dependencies\arm64> Add-AppxPackage .\Microsoft.NET.Native.Framework.2.2.appx
    PS D:\TEMP\UWPDemo1\Dependencies\arm64>

3. 最後に .msixbundle パッケージをインストール

以下のように実行します。

    PS D:\TEMP\UWPDemo1\Dependencies\arm64> Add-AppxPackage .\UWPDemo1_1.0.X.0_x86_x64_arm64.msixbundle
    PS D:\TEMP\UWPDemo1\Dependencies\arm64>

インストール後はタスクバーの検索で UWPDemo1 を検索し実行します。

ただし、LTSCの場合「Windows カメラ」アプリも存在しないため、「Take Picture」を実行してもカメラでのキャプチャは動作しません。
何らかの方法で「Windows カメラ」アプリをインストールすれば「Take Picture」は動作します。

[Windows Camera アプリ](https://www.microsoft.com/store/productId/9WZDNCRFJBBG) 

あるいは (LTSCではなく) SAC 21H2 であればMicrosoft Storeアプリおよびプリインストールの各Storeアプリも存在するので、通常のWindows 10と同様に動作します。

[Windows 10 長期サービス チャネル (LTSC) 次期リリースについて](https://blogs.windows.com/japan/2021/02/25/the-next-windows-10-long-term-servicing-channel-ltsc-release/)

# YouTubeビデオ

このプロジェクトのパッケージは通常のWindows10/11 の x86/x64/ARM64 版にインストールして実行可能ですが、NXP社のi.MX8M PLUS評価ボードで動作している Windows 10 IoT Enterprise 21H2 (ARM64)の上で動かすデモ映像を制作しました。以下のリンクからアクセスしてみて下さい。

https://youtu.be/s8l6BN3Ojog

# アンインストール方法

「設定」アプリの「アプリと機能」で検索欄に UWPDemo1 を入力して検索して見つけた後、「アンインストール」を選択してアンインストールします。

もしインストールしたオレオレ証明書も不要な場合は「コンピューター証明書の管理」管理コンソールアプリで当該証明書を探して削除します。

---

[README.md] version 1.0.4 / 2022年9月
