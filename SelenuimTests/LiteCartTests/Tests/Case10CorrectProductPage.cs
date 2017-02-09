using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCartTests.Tests
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    class Case10CorrectProductPage<TDriver> where TDriver : IWebDriver, new()
    {
        IWebDriver driver;

        [Test]
        public void ProductPageTest()
        {
            driver = new TDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(Scenarios.siteUrl);

            IWebElement firstCampaingProduct = driver.FindElement(By.CssSelector("#box-campaigns .product:first-of-type"));

            string mainPageProductName = firstCampaingProduct.FindElement(By.CssSelector(".name")).Text;
            string mainPageProductRegularPrice = firstCampaingProduct.FindElement(By.CssSelector(".regular-price")).Text;
            string mainPageProductCampaingPrice = firstCampaingProduct.FindElement(By.CssSelector(".campaign-price")).Text;

        }


        [TearDown]
        public void AfterAll()
        {
            driver.Quit();
        }
    }
}
