using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCartTests.Tests
{
    class Case7AllAdminMenu : BaseTest
    {
        [SetUp]
        public void LoginToAdminZone()
        {
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
        }

        [Test]
        public void MenuClickingTest()
        {
            int menuItemsCount = driver.FindElements(By.CssSelector("#box-apps-menu > li")).Count();
            for (int i = 1; i <= menuItemsCount; i++)
            {
                var menuItem = driver.FindElement(By.CssSelector($"#box-apps-menu > li:nth-of-type({i})"));
                menuItem.Click();

                int subMenuItemsCount = driver.FindElements(By.CssSelector("#box-apps-menu > li.selected li")).Count();
                Assert.That(IsHeaderPresent(driver), Is.True, "Header not found!");

                for (int j = 1; j <= subMenuItemsCount; j++)
                {
                    var subMenuItem = driver.FindElement(By.CssSelector($"#box-apps-menu > li.selected li:nth-of-type({j})"));
                    subMenuItem.Click();
                    Assert.That(IsHeaderPresent(driver), Is.True, "Header not found!");
                }
            }
        }

        private bool IsHeaderPresent(IWebDriver driver)
        {
            return driver.FindElements(By.CssSelector("h1")).Count() > 0;
        }
    }
}
