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
    class Case15Grid
    {
        IWebDriver driver;

        [SetUp]
        public void OpenProductsPage()
        {
            driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), DesiredCapabilities.Chrome());
            driver.Navigate().GoToUrl(Scenarios.siteUrl);
        }

        [Test]
        public void GridCheckStickersCountTest()
        {
            var productCards = driver.FindElements(By.CssSelector("li.product"));
            foreach (var card in productCards)
            {
                var stickerCount = card.FindElements(By.CssSelector("div.sticker")).Count();
                Assert.That(stickerCount, Is.EqualTo(1), "Wrong sticker count");
            }
        }

        [TearDown]
        public void AfterTest()
        {
            driver?.Quit();
        }
    }
}
