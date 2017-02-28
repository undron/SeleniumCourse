using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCartTests.Tests
{
    class Case17BrowerLog : BaseTest
    {
        [Test]
        public void BrowserLogTest()
        {
            //Step 1
            Scenarios.LoginToAdminZone(driver);

            //Step 2
            driver.Navigate().GoToUrl(Scenarios.siteAdminUrl + "?app=catalog&doc=catalog&category_id=1");

            //Step 3
            var productsLocator = By.CssSelector(".dataTable .row td:nth-of-type(3) a[href*='product_id']");
            int productsCount = driver.FindElements(productsLocator).Count;
            for (int i = 0; i < productsCount; i++)
            {
                driver.FindElements(productsLocator)[i].Click();
                var logs = driver.Manage().Logs.GetLog("browser");

                Assert.That(logs, Is.Empty);
                driver.Navigate().Back();
            }
        }
    }
}
