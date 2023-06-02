# Standalone Build with CSharp Tests

This repository shows a few C# tests that use the page object model and AltTester to test the Unity endless runner sample:
https://assetstore.unity.com/packages/essentials/tutorial-projects/endless-runner-sample-game-87901

## NuGet package

**This project already has the AltDriver inside, but otherwise would require to add https://www.nuget.org/packages/AltTester-Driver package in order to work.**

### Running the tests on Windows or MacOS
The tests are meant to be run on an Windows or MacOS device.
Create a folder `App` under EXAMPLES-CSharp-Standalone-AltTrashCat folder.

To start the tests, depending of your OS run:

- `./start_tests_Mac.sh` on MacOS/Linux

    Create a folder `TrashCatMac` under `App`.
    The app is provided at https://altom.com/app/uploads/AltTester/TrashCat/TrashCatMacOS.app.zip and needs to be included unzipped under the App/TrashCatMac/ folder.

- `./start_tests_Windows.sh` on Windows

    Create a folder `TrashCatWindows` under `App`.
    The app is provided at https://altom.com/app/uploads/AltTester/TrashCat/TrashCatWindows.zip and needs to be included unzipped under the App/TrashCatWindows/ folder.
    
    Or you can follow all the steps from start_test_Windows.sh.
    
This script will:

- start the app on your device
- run the tests
- stop the app after the test are done

You can view a video of how to run the tests on MAC OS by clicking on the following image: 

[![Youtube](http://img.youtube.com/vi/tr3_8YawBck/0.jpg)](https://www.youtube.com/embed/tr3_8YawBck "Youtube")

## Running the tests on Android

1. Have an [instrumented build for Android.](https://alttester.com/docs/sdk/pages/get-started.html#instrument-your-game-with-alttester-unity-sdk)
2. Download and install [.NET SDK](https://dotnet.microsoft.com/en-us/download).
3. Have [AltTester Desktop app](https://alttester.com/alttester/) installed (to be able to inspect game)
4. Download and install [ADB for Windows](https://dl.google.com/android/repository/platform-tools-latest-windows.zip).
5. Enable Developers Options on mobile device [more instructions here](https://www.xda-developers.com/install-adb-windows-macos-linux/).

This repository is a test project that uses NUnit as the test library. It was generated using the following command (as suggested in [documentation](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-nunit#creating-the-test-project)):
```
dotnet new nunit
```
[AltTester Unity SDK framework](https://alttester.com/docs/sdk/) contains AltDriver class used to connect to the instrumented game developed w/ Unity. AltTester-Driver for C# is available as a nuget package. Install [AltTester-Driver nuget package](https://www.nuget.org/packages/AltTester-Driver#versions-body-tab).
```
dotnet add package AltTester-Driver --version 1.8.2
```

### Android setup

To add a device to the device list using ADB in VS Code, follow these steps:

1. Make sure you have installed ADB (Android Debug Bridge) on your computer. If you haven't installed it yet, you can download the platform-tools from the official Android SDK website and extract the contents to a convenient directory.

2. Connect your Android device to the computer using a USB cable. Ensure that USB debugging mode is enabled on the device.

If you're receiving the "adb: command not found" error, it means that the adb executable is not recognized by your system. Here are a few steps you can follow to resolve this issue:

- Check ADB Installation: Ensure that you have installed ADB (Android Debug Bridge) on your computer. If you haven't installed it yet, you'll need to download the Android SDK Platform-Tools from the official Android developer website and extract the contents to a convenient directory.

- Add ADB to System Path: Add the directory containing the ADB executable to your system's PATH environment variable. This allows your system to locate and execute the adb command from any location in the terminal.

3.  Make sure the mobile device is connected via USB, then execute the following command:
```
adb devices
```

4. On the mobile device, allow USB Debugging access (RSA key fingerprint from the computer).

To uninstall the app from the device, use the following command:
```
adb uninstall com.Altom.TrashCat
```

To install the app on the device, use the following command:
```
adb install TrashCat.apk
```

