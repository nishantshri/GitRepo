1. To run the End to End UI test case IIS express should be installed on the system.

2. End to End UI test case will use port 5886 to run the application in browser. If this post is busy in the system
please change the Port number to any other value (that port should be free). 
To change the port number go to NumericSequenceWorldNomadsTest -> Framework -> Host.cs and 
in this line of code Instance.Run("NumericSequenceWorldNomads", 5886); change value of 5886.

3. Test cases for all use cases are available in HomeControllerTest folder of NumericSequenceWorldNomadsTest project.

4. End to End UI test case is availabe in UITests folder of NumericSequenceWorldNomadsTest project.

5. End to End UI test case uses Selenium web driver and Chromedriver.exe. Chrome will be the browser to run End to End UI test.

6.In case test cases are not running and following error is coming "Could not load file or assembly HRESULT: 0x80131515"
Please perform the below steps:
  6.1 Go to install path of Visual Studio (For eg: C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE) and open devenv.exe.xml
    in any text editor. Text editor should be opened as Administrator.
  6.2 Just below runtime tag add loadFromRemoteSources enabled="true".
  
7. Application is developed in Visual Studio 2010 and framework is .NET 4.0

8. MVC framwork has been used to developed the web application.

9. In case of any issues while running the application please contact me.



