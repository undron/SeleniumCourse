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
    public class Case11Register : BaseTest
    {
        [SetUp]
        public void OpenProductsPage()
        {
            driver.Navigate().GoToUrl(Scenarios.siteUrl);
        }

        [Test]
        public void RegisterUserTest()
        {
            string loginEmail = GenerateEmail();
            string password = "password";

            //Open registation page
            driver.FindElement(By.CssSelector("#box-account-login a")).Click();

            //Fill registration form and click 'Create Account'
            driver.FindElement(By.Name("firstname")).SendKeys("Testname");
            driver.FindElement(By.Name("lastname")).SendKeys("Testlastname");
            driver.FindElement(By.Name("address1")).SendKeys("Somewhere, Some str., 1, 1");
            driver.FindElement(By.Name("postcode")).SendKeys("12345");
            driver.FindElement(By.Name("city")).SendKeys("Nowhere");
            SetCountryByCode("US");
            SetZoneByCode("AK");
            driver.FindElement(By.Name("email")).SendKeys(loginEmail);
            driver.FindElement(By.Name("phone")).SendKeys("+123456789");
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("confirmed_password")).SendKeys(password);
            driver.FindElement(By.Name("create_account")).Click();

            //Logout
            driver.FindElement(By.LinkText("Logout")).Click();

            //Login
            driver.FindElement(By.Name("email")).SendKeys(loginEmail);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();

            //Logout
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        private string GenerateEmail()
        {
            return DateTime.Now.Ticks + "@mail.com";
        }

        private void SetCountryByCode(string countryCode)
        {
            driver.FindElement(By.CssSelector(".select2-container")).Click();
            driver.FindElement(By.CssSelector($".select2-results__options li[id$={countryCode}]")).Click();
        }

        private void SetZoneByCode(string zoneCode)
        {
            var zoneSelect = driver.FindElement((By.CssSelector("select[name='zone_code']")));
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                    .Until(ExpectedConditions.ElementToBeClickable(zoneSelect));
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.WriteLine("Zone select is not enabled. Has country any zones?");
                return;
            }
            new SelectElement(zoneSelect).SelectByValue(zoneCode);
        }
    }
}
