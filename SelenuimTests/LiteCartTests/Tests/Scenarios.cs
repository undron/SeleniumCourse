using OpenQA.Selenium;

namespace LiteCartTests.Tests
{
    public class Scenarios
    {
        public static string siteUrl = $"http://localhost{GetPort()}/litecart/";
        public static string siteAdminUrl = $"http://localhost{GetPort()}/litecart/admin/";
        public static string login = "admin";
        public static string password = "admin";

        public static void LoginToAdminZone(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(siteAdminUrl);
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("login")).Click();
        }

        private static bool AtWork() => System.Environment.MachineName == "MINBOPC037";
        private static string GetPort() => AtWork() ? ":8081" : "";
    }
}
