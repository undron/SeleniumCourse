using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCartTests.Tests
{
    public class Scenarios
    {
        public static string siteUrl = "http://localhost:8081/litecart/";
        public static string siteAdminUrl = "http://localhost:8081/litecart/admin/";
        public static string login = "admin";
        public static string password = "admin";

        public static void LoginToAdminZone(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(siteAdminUrl);
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
        }
    }
}
