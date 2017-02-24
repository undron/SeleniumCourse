using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCartTests.Tests
{
    class Case16Browserstack
    {
        IWebDriver driver;

        [SetUp]
        public void OpenProductsPage()
        {
            DesiredCapabilities capability = new DesiredCapabilities();
            capability.SetCapability("browser", "Chrome");
            capability.SetCapability("browser_version", "56.0");
            capability.SetCapability("os", "Windows");
            capability.SetCapability("os_version", "7");
            capability.SetCapability("resolution", "1024x768");

            capability.SetCapability("browserstack.user", "");
            capability.SetCapability("browserstack.key", "");
            capability.SetCapability("browserstack.local", "true");

            driver = new RemoteWebDriver(new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability);

            driver.Navigate().GoToUrl(Scenarios.siteUrl);
        }

        [Test]
        public void BrowserstackLoginToAdminZone()
        {
            Scenarios.LoginToAdminZone(driver);
        }

        [TearDown]
        public void AfterTest()
        {
            driver?.Quit();
        }
    }
}
