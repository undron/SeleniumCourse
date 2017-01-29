using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace LiteCartTests
{
    public class FirstSteps
    {
        IWebDriver driver;

        [SetUp]
        public void BeforeTest()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void JustOpenBrowserTest()
        {
            driver.Navigate().GoToUrl("http://www.tut.by");
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Close();
        }
    }
}
