# azure-ai-ocr-demo
Windows UWP project for demonstrating Microsoft Azure Cognitive Read API

���̃v���W�F�N�g��Microsoft Azure �� Cognitive Services �̂����AComputer Vision OCR �����s����GUI�f���v���O�����ł��B

C# �L�q��UWP�v���W�F�N�g�ŁA.NET Native �ɂ�� x86/x64/ARM64 �� .msixbundle �t�@�C���𐶐����܂��B


# �J����

Windows 10 version 21H2 (64bit) ��� Visual Studio 2019 version 16.11 ���C���X�g�[�����ĊJ�����܂����B����Visual Studio 2019 Comunity Edition�𗘗p�������ꍇ�́A������Visual Studio Dev Essentials �v���O�����ɉ�������ƁAVisual Studio Subscription�T�C�g����_�E�����[�h�\�ł��B

Visual Studio Dev Essentials
https://visualstudio.microsoft.com/ja/dev-essentials/

�_�E�����[�h�y�[�W (�v���O�C��)
https://my.visualstudio.com/Downloads?q=Visual%20Studio%202019

Visual Studio Installer �ł� ���[�N���[�h�Ƃ��āu���j�o�[�T��Windows�v���b�g�t�H�[���J���v�u.NET�f�X�N�g�b�v�J���v��I�����܂��B�܂��A�ʂ̃R���|�[�l���g�Ƃ��āuGit for Windows�v��I��ŃC���X�g�[�����Ă����ƕ֗��ł��B

# Azure �|�[�^���Ń��\�[�X���쐬���AENDPOINT����KEY�����擾����

�ȉ����Q�l��[Azure �|�[�^��](https://portal.azure.com/#create/Microsoft.CognitiveServicesComputerVision)��Azure Cognitive Services�̃��\�[�X���쐬���܂��B

https://docs.microsoft.com/ja-jp/azure/cognitive-services/computer-vision/quickstarts-sdk/client-library?tabs=visual-studio&pivots=programming-language-csharp#prerequisites

���\�[�X���쐬������A�u�L�[�ƃG���h�|�C���g�v�ŕ\������鍀�ڂ̂���

- �L�[1 �܂��� �L�[2 (�ǂ���ł��悢)
- �G���h�|�C���g

��2�̏����������Ă����܂��B

�ڂ������@�͈ȉ��̃g���[�j���O���K������Ă݂邱�Ƃł������ł��܂��B

[Computer Vision �T�[�r�X���g�p���ĉ摜��h�L�������g���̃e�L�X�g��ǂݎ��](https://docs.microsoft.com/ja-jp/learn/modules/read-text-images-documents-with-computer-vision-service/)


# GitHub����\�[�X�R�[�h�v���W�F�N�g�� clone ���ĊJ��

Visual Studio 2019 ���N����A�uclone a repository�v��I��� https://github.com/codegear-official/azure-ai-ocr-demo.git ����͂���� ���[�J����clone������v���W�F�N�g���J���܂��B

https://github.com/codegear-official/azure-ai-ocr-demo.git

�r���h Configuration�̑I���� �uDebug�v�ux64�v��I��� Build ���j���[����ubuild UWPDemo1�v�����s����ƃr���h�J�n���܂��B���΂炭���ăr���h��������ƁAOutput �E�C���h�E��Build��

    ========== Build: 1 succeeded, 0 failed, 0 up-to-date, 0 skipped ==========

�̂悤�ɕ\������A�r���h���������܂��B

# �J��PC�Ńf�o�b�O���s

Debug ���j���[����uStart Debugging�v��I�Ԃ��AF5�L�[�������ƃf�o�b�O���s���J�n���܂��B����̓f�o�b�O�V���{�����_�E�����[�h����̂ŁA�N���Ɏ��Ԃ�������܂� (�X�L�b�v���\)�B

�N��������܂� �uSetting�v���j���[����uOpen Config�v��I��Őݒ�t�@�C�� appsettings.json �̂���f�B���N�g�����J���܂��B

�����A�ȉ��̓��e�� Json�t�@�C���������Ă���̂ŁA���ۂɗ��p�\�� ENDPOINT��KEY���ɏ��������܂��B

    {
        "CognitiveServicesEndpoint": "YOUR_COGNITIVE_SERVICES_ENDPOINT",
        "CognitiveServiceKey": "YOUR_COGNITIVE_SERVICES_KEY"
    }
    
**�ύX��̗�**

    {
        "CognitiveServicesEndpoint": "https://XXXXXXXX.cognitiveservices.azure.com/",
        "CognitiveServiceKey": "2c62dea8fc12489f8300f5bcbbe0e493"
    }

�t�@�C����������������Explorer�E�C���h�E���N���[�Y���܂��B

���̌�A�uPick�v��I��� OCR�������s���摜�t�@�C����I�����邩�B�uTake Picture�v��I��ŁA�J�����f�o�C�X����OCR�����s����摜���L���v�`�����܂��B

����OCR����������������A�R�}���h�o�[��� SUCCESS �̕������\������܂��B
�摜���X�N���[�����ĉ��̕�����\������ƁA�F�����ꂽ�����񂪕\������Ă��邱�Ƃ��킩��܂��B

�E�C���h�E�E��̃N���[�Y�{�^�� (�~��) ���N���b�N����ƁA�f���A�v�����I�����܂��B

# ����PC�Ŏ��s���邽�߂Ƀp�b�P�[�W���r���h

��������ɖ��Ȃ���΁AProject���j���[�́uPublish�v����uCreate App Packages...�v��I�т܂��B

���̃_�C�A���O�𗘗p���� Microsoft Store�ɒ�o���邽�߂̃p�b�P�[�W���쐬������A�T�C�h���[�f�B���O�p��.msixbundle �t�@�C�����r���h���邱�Ƃ��ł��܂��B

�T�C�h���[�f�B���O�p�̃p�b�P�[�W���r���h����ꍇ�A�ݒ�_�C�A���O�ŁuSideloading�v��I���� Next�����s�B
���̉�ʂŏ����p�̏ؖ�����ݒ肵�܂��B�v���W�F�N�g���ɂ͂�����ō쐬���� .pfx�t�@�C�����܂܂�Ă��܂����A�v���W�F�N�g����폜���ĐV�K��.pfx���쐬���邱�Ƃ��\�ł��BNext�������܂��B

���̉�ʂŃo�[�W��������уp�b�P�[�W�Ɋ܂߂�CPU�^�C�v���`�F�b�N���Ďw�肵�܂��B���ꂼ���CPU�^�C�v�ŁA�Ȃ���������Ԃ�Debug �r���h���I������Ă���̂ŁA�蓮�Ń`�F�b�N�̓����Ă��鍀�ڂ��ׂĂ�Release �r���h�̑I���ɕύX���ACreate �������܂��B���΂炭����ƃp�b�P�[�W���r���h����A�ŏI�I�Ƀr���h���ʂ̃t�@�C�����܂܂��f�B���N�g�����J�������N���\�������̂ŁA������N���b�N�����Explorer�ňȉ��̃t�@�C�����܂܂��f�B���N�g�����\������܂��B

- UWPDemo1_1.0.X.0_x86_x64_arm64.msixbundle : �C���X�g�[���\�ȃp�b�P�[�W�t�@�C��
- UWPDemo1_1.0.X.0_x86_x64_arm64.cer : �ؖ����t�@�C�� (������I���I���ؖ���)
- UWPDemo1_1.0.4.0_XXX.appxsym : �f�o�b�O�V���{���t�@�C��
- Dependencies : �ˑ����W���[���̊܂܂��f�B���N�g��

# ��PC�ɃR�s�[���ăC���X�g�[��

�ʏ�� Windows�ł́A�p�b�P�[�W�̂����uUWPDemo1_1.0.X.0_x86_x64_arm64.msixbundle�v�uUWPDemo1_1.0.X.0_x86_x64_arm64.cer�v��2�̃t�@�C�����R�s�[���Ă����ƃT�C�h���[�f�B���O�p�̃C���X�g�[�����ł��܂��B (���łɂ��炩���߃p�����[�^��ݒ肵�� appsettings.json ���R�s�[���Ă����ƕ֗�)

1. �I���I���ؖ������ؖ����X�g�A�ɃC���X�g�[��

�uUWPDemo1_1.0.X.0_x86_x64_arm64.cer�v���_�u���N���b�N���ĊJ���ƁA�_�C�A���O���\������u�ؖ����̃C���X�g�[��...�v�{�^�����\�������̂ŁA������N���b�N���ăC���X�g�[�����܂��B�C���X�g�[����͕K���u���[�J���R���s���[�^�[�v�́u�M�����ꂽ���[�g�ؖ��@�ցv�̏ꏊ�ɂ���K�v������܂��B

�C���X�g�[����Ɂu�R���s���[�^�[�ؖ����̊Ǘ��v�A�v�������s���A�u�M�����ꂽ���[�g�ؖ��@�ցv�̉��́u�ؖ����v�̈ꗗ�̒��ɔ��s��Ɣ��s�҂����Ɂuhron�v�ɂȂ��Ă���ؖ�����������Ǝv���܂��B���̃e�X�g�p�I���I���ؖ����͗L��������1�N�ɐݒ肳��Ă���̂ŁA�ؖ����L���Ȃ̂͏ؖ���������A�ő�1�N�ԂƂ������ƂɂȂ�܂��B

2. �A�v�����C���X�g�[��

�uUWPDemo1_1.0.X.0_x86_x64_arm64.msixbundle�v���_�u���N���b�N����ƁA�A�v���C���X�g�[���[������������܂��B�u�C���X�g�[���v�����s����ƃC���X�g�[������A����Ɂu�������o������N���v�̃`�F�b�N�������Ă�����A�C���X�g�[���I����ɂ����ɃA�v���������オ��܂��B

# LTSC�̏ꍇ�̃C���X�g�[�����@

�uWindows 10 Enterprise LTSC�v�ƁuWindows 10 IoT Enterprise LTSC�v�̏ꍇ�́A�A�v���C���X�g�[���[���̂����݂��Ȃ����߁AExplorer�� .msixbundle �t�@�C�����_�u���N���b�N���Ă������N�����C���X�g�[������܂���B

���̂悤�ȏꍇ�A**�Ǘ��҃��[�h��** PowerShell�ŁuAdd-AppxPackage�v�R�}���h�𗘗p���ăC���X�g�[�����邱�Ƃ��ł��܂��B�������A���̎蓮�C���X�g�[���̕��@�ł͈ˑ��֌W���W���[�����蓮�ŃC���X�g�[������K�v������܂��B�ȉ��̎菇�ŃC���X�g�[�����܂��B

1. �I���I���ؖ����t�@�C���̃C���X�g�[��

����� LTSC�ȊO�̏ꍇ�Ɠ������@�ŃC���X�g�[�����܂��B

2. �ˑ��֌W���W���[���� Dependencies �f�B���N�g������R�s�[���Ă��ăC���X�g�[��

��̓I�ɂ͈ȉ��̂悤�Ɏ��s���܂��B (�ȉ���ARM64�̏ꍇ)

    PS D:\TEMP\UWPDemo1\Dependencies\arm64> dir

        �f�B���N�g��: D:\TEMP\UWPDemo1\Dependencies\arm64

    Mode                 LastWriteTime         Length Name
    ----                 -------------         ------ ----
    -a----        2022/08/27      0:28        5172939 Microsoft.NET.Native.Framework.2.2.appx
    -a----        2022/08/27      0:28         248183 Microsoft.NET.Native.Runtime.2.2.appx
    -a----        2022/08/27      0:28        1529007 Microsoft.VCLibs.ARM64.14.00.appx

    PS D:\TEMP\UWPDemo1\Dependencies\arm64> Add-AppxPackage .\Microsoft.VCLibs.ARM64.14.00.appx
    PS D:\TEMP\UWPDemo1\Dependencies\arm64> Add-AppxPackage .\Microsoft.NET.Native.Runtime.2.2.appx
    PS D:\TEMP\UWPDemo1\Dependencies\arm64> Add-AppxPackage .\Microsoft.NET.Native.Framework.2.2.appx
    PS D:\TEMP\UWPDemo1\Dependencies\arm64>

3. �Ō�� .msixbundle �p�b�P�[�W���C���X�g�[��

PS D:\TEMP\UWPDemo1\Dependencies\arm64> Add-AppxPackage .\UWPDemo1_1.0.X.0_x86_x64_arm64.msixbundle
PS D:\TEMP\UWPDemo1\Dependencies\arm64>

�C���X�g�[����̓^�X�N�o�[�̌����� UWPDemo1 �����������s���܂��B

�������ALTSC�̏ꍇ�uWindows �J�����v�A�v�������݂��Ȃ����߁A�uTake Picture�v�����s���Ă��J�����ł̃L���v�`���͓��삵�܂���B
���炩�̕��@�ŁuWindows �J�����v�A�v�����C���X�g�[������΁uTake Picture�v�͓��삵�܂��B

[Windows Camera �A�v��](https://www.microsoft.com/store/productId/9WZDNCRFJBBG) 

# �A���C���X�g�[�����@

�u�ݒ�v�A�v���́u�A�v���Ƌ@�\�v�Ō������� UWPDemo1 ����͂��Č���������A�u�A���C���X�g�[���v��I�����ăA���C���X�g�[�����܂��B

�����ؖ������s�v�ȏꍇ�́u�R���s���[�^�[�ؖ����̊Ǘ��v�A�v���œ��Y�ؖ�����T���č폜���܂��B

---

[README.md] version 1.0.4 / 2022�N9��



