using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace LiteCartTests.Tests
{
    class LoginToAdminZone
    {
        IWebDriver driver;

        string siteUrl = "http://localhost/litecart/admin/";
        string login = "admin";
        string password = "admin";

        [SetUp]
        public void BeforeTest()
        {
            driver = new ChromeDriver();
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
