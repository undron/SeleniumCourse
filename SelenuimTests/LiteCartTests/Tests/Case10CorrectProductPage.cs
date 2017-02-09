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
        By firstCampaingProductLocator = By.CssSelector("#box-campaigns .product:first-of-type");

        [OneTimeSetUp]
        public void CreateDriver()
        {
            driver = new TDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }

        [SetUp]
        public void OpenMainPage()
        {
            driver.Navigate().GoToUrl(Scenarios.siteUrl);
        }

        [Test]
        public void ProductNameTest()
        {
            IWebElement firstCampaingProduct = driver.FindElement(firstCampaingProductLocator);

            string mainPageProductName = firstCampaingProduct.FindElement(By.CssSelector(".name")).Text;
            firstCampaingProduct.Click();
            string productPageProductName = driver.FindElement(By.CssSelector("#box-product .title")).Text;

            Assert.That(productPageProductName, Is.EqualTo(mainPageProductName));
        }

        [Test]
        public void ProductPriceTest()
        {
            IWebElement firstCampaingProduct = driver.FindElement(firstCampaingProductLocator);

            string mainPageProductRegularPrice = firstCampaingProduct.FindElement(By.CssSelector(".regular-price")).Text;
            string mainPageProductCampaingPrice = firstCampaingProduct.FindElement(By.CssSelector(".campaign-price")).Text;
            firstCampaingProduct.Click();
            string productPageProductRegularPrice = driver.FindElement(By.CssSelector("#box-product .regular-price")).Text;
            string productPageProductCampaingPrice = driver.FindElement(By.CssSelector("#box-product .campaign-price")).Text;

            Assert.That(productPageProductRegularPrice, Is.EqualTo(mainPageProductRegularPrice));
            Assert.That(productPageProductCampaingPrice, Is.EqualTo(mainPageProductCampaingPrice));
        }

        [Test]
        public void ProductPriceStyleTest()
        {
            IWebElement firstCampaingProduct = driver.FindElement(firstCampaingProductLocator);

            IWebElement regularPrice = firstCampaingProduct.FindElement(By.CssSelector(".regular-price"));

            string regularPriceColor = regularPrice.GetCssValue("color");
            Assert.That(IsColorGrey(regularPriceColor), Is.True, $"Color {regularPriceColor} is not grey");

            string regularPriceTag = regularPrice.GetAttribute("tagName");
            Assert.That(regularPriceTag, Is.EqualTo("s").IgnoreCase);

            IWebElement campaignPrice = firstCampaingProduct.FindElement(By.CssSelector(".campaign-price"));

            string campaignPriceColor = campaignPrice.GetCssValue("color");
            Assert.That(IsColorRed(campaignPriceColor), Is.True, $"Color {campaignPriceColor} is not red");

            string campaignPriceTag = campaignPrice.GetAttribute("tagName");
            Assert.That(campaignPriceTag, Is.EqualTo("strong").IgnoreCase);

            firstCampaingProduct.Click();

            regularPrice = driver.FindElement(By.CssSelector("#box-product .regular-price"));

            regularPriceColor = regularPrice.GetCssValue("color");
            Assert.That(IsColorGrey(regularPriceColor), Is.True, $"Color {regularPriceColor} is not grey");

            regularPriceTag = regularPrice.GetAttribute("tagName");
            Assert.That(regularPriceTag, Is.EqualTo("s").IgnoreCase);

            campaignPrice = driver.FindElement(By.CssSelector("#box-product .campaign-price"));

            campaignPriceColor = campaignPrice.GetCssValue("color");
            Assert.That(IsColorRed(campaignPriceColor), Is.True, $"Color {campaignPriceColor} is not red");

            campaignPriceTag = campaignPrice.GetAttribute("tagName");
            Assert.That(campaignPriceTag, Is.EqualTo("strong").IgnoreCase);
        }

        [Test]
        public void ProductCampaingPriceStyleTest()
        {
            IWebElement firstCampaingProduct = driver.FindElement(firstCampaingProductLocator);

            string regularPriceFontSize = firstCampaingProduct.FindElement(By.CssSelector(".regular-price"))
                .GetCssValue("font-size");
            string campaignPriceFontSize = firstCampaingProduct.FindElement(By.CssSelector(".campaign-price"))
                .GetCssValue("font-size");
            Assert.That(ParseFontSize(campaignPriceFontSize), Is.GreaterThan(ParseFontSize(regularPriceFontSize)));

            firstCampaingProduct.Click();

            regularPriceFontSize = driver.FindElement(By.CssSelector("#box-product .regular-price"))
                .GetCssValue("font-size");
            campaignPriceFontSize = driver.FindElement(By.CssSelector("#box-product .campaign-price"))
                .GetCssValue("font-size");
            Assert.That(ParseFontSize(campaignPriceFontSize), Is.GreaterThan(ParseFontSize(regularPriceFontSize)));
        }

        private bool IsColorGrey(string htmlColor)
        {
            var parsed = ParseColors(htmlColor);
            return parsed[0] == parsed[1] && parsed[1] == parsed[2];
        }

        private bool IsColorRed(string htmlColor)
        {
            var parsed = ParseColors(htmlColor);
            return parsed[0] > 0 && parsed[1] == 0 && parsed[2] == 0;
        }

        //Different lenght for Chrome/IE and FF, ignoring
        private List<byte> ParseColors(string htmlColor)
        {
            string color;
            if (htmlColor.Contains("rgba"))
                color = htmlColor.Replace("rgba(", "").Replace(")", "");
            else if (htmlColor.Contains("rgb"))
                color = htmlColor.Replace("rgb(", "").Replace(")", "");
            else
                throw new ArgumentException();

            return color.Split(',').Select(s => s.Trim()).Select(b => byte.Parse(b)).ToList();
        }

        private float ParseFontSize(string htmlFontSize)
        {
            if (!htmlFontSize.Contains("px"))
                throw new ArgumentException();

            return float.Parse(htmlFontSize.Replace("px", ""), System.Globalization.CultureInfo.InvariantCulture);
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            driver.Quit();
        }
    }
}
