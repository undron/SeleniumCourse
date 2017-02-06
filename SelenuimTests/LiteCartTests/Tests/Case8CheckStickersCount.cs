using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCartTests.Tests
{
    class Case8CheckStickersCount : BaseTest
    {
        [SetUp]
        public void OpenProductsPage()
        {
            driver.Navigate().GoToUrl(siteUrl);
        }

        [Test]
        public void CheckStickersCountTest()
        {
            var productCards = driver.FindElements(By.CssSelector("li.product"));
            foreach (var card in productCards)
            {
                var stickerCount = card.FindElements(By.CssSelector("div.sticker")).Count();
                Assert.That(stickerCount, Is.EqualTo(1), "Wrong sticker count");
            }
        }
    }
}
