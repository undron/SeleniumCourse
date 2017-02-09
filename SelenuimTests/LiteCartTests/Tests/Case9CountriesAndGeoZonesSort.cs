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
    class Case9CountriesAndGeoZonesSort<TDriver> where TDriver : IWebDriver, new()
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void LoginToAdminZone()
        {
            driver = new TDriver();
            driver.Manage().Window.Maximize();
            Scenarios.LoginToAdminZone(driver);
        }

        [Test]
        public void CountriesSortTest()
        {
            string countriesPageUrl = Scenarios.siteAdminUrl + "?app=countries&doc=countries";

            driver.Navigate().GoToUrl(countriesPageUrl);
            var countries = driver.FindElements(By.CssSelector(".dataTable .row td:nth-of-type(5)"))
                .Select(element => element.Text);
            TestContext.WriteLine("Countries found:\n" + String.Join("\n", countries));
            Assert.That(countries, Is.Ordered);

            var countriesWithZonesLocator = 
                By.XPath("//*[@class='dataTable']//tr[@class='row']/td[6][not(contains(.,'0'))]/../td[5]/a");
            var countriesWithZonesCount = driver.FindElements(countriesWithZonesLocator).Count;

            for (int i = 0; i < countriesWithZonesCount; i++)
            {
                driver.Navigate().GoToUrl(countriesPageUrl);
                driver.FindElements(countriesWithZonesLocator)[i].Click();

                var zones = driver.FindElements(By.CssSelector("#table-zones td:nth-of-type(3)>input[type='hidden']"))
                    .Select(element => element.GetAttribute("value"));
                TestContext.WriteLine("\n\nZones found:\n" + String.Join("\n", zones));
                Assert.That(zones, Is.Ordered);
            }
        }

        [Test]
        public void GeoZonesSortTest()
        {
            string geoZonesPageUrl = Scenarios.siteAdminUrl + "?app=geo_zones&doc=geo_zones";

            driver.Navigate().GoToUrl(geoZonesPageUrl);

            var countriesWithZonesLocator = By.CssSelector(".dataTable .row>td:nth-of-type(3) a");
            var countriesWithZonesCount = driver.FindElements(countriesWithZonesLocator).Count;

            for (int i = 0; i < countriesWithZonesCount; i++)
            {
                driver.Navigate().GoToUrl(geoZonesPageUrl);
                driver.FindElements(countriesWithZonesLocator)[i].Click();

                var zones = driver.FindElements(By.CssSelector("#table-zones td:nth-of-type(3)>select>[selected]"))
                    .Select(element => element.Text);
                TestContext.WriteLine("\n\nZones found:\n" + String.Join("\n", zones));
                Assert.That(zones, Is.Ordered);
            }
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            driver.Quit();
        }
    }
}
