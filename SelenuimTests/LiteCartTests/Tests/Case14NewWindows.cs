using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCartTests.Tests
{
    class Case14NewWindows : BaseTest
    {
        [SetUp]
        public void LoginToAdminZone()
        {
            //Step 1
            Scenarios.LoginToAdminZone(driver);
        }

        [Test]
        public void NewWindowsTest()
        {
            //Step 2
            driver.FindElement(By.CssSelector("[href$='?app=countries&doc=countries']")).Click();

            //Step 3
            driver.FindElement(By.CssSelector("a[href$='?app=countries&doc=edit_country']")).Click();

            //Step 4
            var externalLinks = driver.FindElements(By.CssSelector(".fa-external-link"));
            string mainWindow = driver.CurrentWindowHandle;
            for (int i = 0; i < externalLinks.Count; i++)
            {
                externalLinks[i].Click();
                string newWindow = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                    .Until(ThereIsWindowOtherThan(mainWindow));
                driver.SwitchTo().Window(newWindow);

                //Just for fun
                string title = driver.Title;
                Assert.That(title, Is.Not.Empty);

                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }
        }

        private Func<IWebDriver, string> ThereIsWindowOtherThan(string oldWindow)
        {
            return (driver) =>
            {
                var windows = driver.WindowHandles;
                return windows.FirstOrDefault(window => window != oldWindow);
            };
        }
    }
}
