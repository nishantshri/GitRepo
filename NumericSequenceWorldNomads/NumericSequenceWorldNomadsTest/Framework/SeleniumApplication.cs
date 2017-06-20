using System;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;

namespace NumericSequenceWorldNomadsTest.Framework
{
    internal class SeleniumApplication
    {
        private IisExpressWebServer _webServer;
        private RemoteWebDriver _webDriver;

        public RemoteWebDriver WebDriver { get { return _webDriver; } }
        public IisExpressWebServer WebServer { get { return _webServer; } }

        public void Run(string webProjectFolder, int portNumber)
        {
            var webApplication = new WebApplication(ProjectLocation.FromFolder(webProjectFolder), portNumber);
            try
            {
                _webServer = new IisExpressWebServer(webApplication);
                _webServer.Start();
                _webDriver = new ChromeDriver();
                _webDriver.Navigate().GoToUrl(_webServer.BaseUrl);
            }
            catch (Exception ex)
            {
                
                throw;
            }
            

            AppDomain.CurrentDomain.DomainUnload += CurrentDomainDomainUnload;
        }

        private void CurrentDomainDomainUnload(object sender, EventArgs e)
        {
            _webDriver.Close();
            _webServer.Stop();
        }
    }
}
