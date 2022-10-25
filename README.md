# Standalone Build with CSharp Tests

This repository shows a few C# tests that use the page object model and AltTester to test the Unity endless runner sample:
https://assetstore.unity.com/packages/essentials/tutorial-projects/endless-runner-sample-game-87901

## NuGet package

**This project already has the AltDriver inside, but otherwise would require to add https://www.nuget.org/packages/AltDriver package in order to work.**

### Running the tests on Windows or MacOS
The tests are meant to be run on an Windows or MacOS device.

To start the tests, depending of your OS run:

- `./start_tests_Mac.sh` on MacOS/Linux
    The app is provided at https://altom.com/app/uploads/AltTester/TrashCat/TrashCatMacOS.zip and needs to be included  unzipped under the App/TrashCatMac/ folder.

- `./start_tests_Windows.sh`./start_tests_windows.sh on Windows
    The app is provided at https://altom.com/app/uploads/AltTester/TrashCat/TrashCatWindows.zip and needs to be included  unzipped under the App/TrashCatWindows/ folder.
    
This script will:

- start the app on your device
- run the tests
- stop the app after the test are done

You can view a video of how to run the tests on MAC OS by clicking on the following image: 

[![Youtube](http://img.youtube.com/vi/tr3_8YawBck/0.jpg)](https://www.youtube.com/embed/tr3_8YawBck "Youtube")
