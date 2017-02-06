using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCartTests.Tests
{
    public abstract class BaseTest
    {
        protected IWebDriver driver;

        protected string siteUrl = "http://localhost/litecart/admin/";
        protected string login = "admin";
        protected string password = "admin";

        [SetUp]
        public void BeforeTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(siteUrl);
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }
    }
}
