using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace LiteCartTests.Tests
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    class LoginToAdminZone<TDriver> where TDriver : IWebDriver, new()
    {
        IWebDriver driver;

        string siteUrl = "http://localhost/litecart/admin/";
        string login = "admin";
        string password = "admin";

        [SetUp]
        public void BeforeTest()
        {
            driver = new TDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(siteUrl);
        }

        [Test]
        public void LoginToAdmin()
        {
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }
    }

    [TestFixture(true, @"c:\Program Files (x86)\Mozilla Firefox ESR\firefox.exe")]
    [TestFixture(false, @"c:\Program Files\Nightly\firefox.exe")]
    class LoginToAdminZoneInForefox
    {
        IWebDriver driver;

        string siteUrl = "http://localhost/litecart/admin/";
        string login = "admin";
        string password = "admin";
        bool legacy;
        string binaryLocation;

        public LoginToAdminZoneInForefox(bool legacy, string binaryLocation)
        {
            this.legacy = legacy;
            this.binaryLocation = binaryLocation;
        }

        [SetUp]
        public void BeforeTest()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.UseLegacyImplementation = legacy;
            options.BrowserExecutableLocation = binaryLocation;
            driver = new FirefoxDriver(options);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl(siteUrl);
        }

        [Test]
        public void LoginToAdmin()
        {
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }
    }
}
