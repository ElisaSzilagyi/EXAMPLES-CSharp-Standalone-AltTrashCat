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
