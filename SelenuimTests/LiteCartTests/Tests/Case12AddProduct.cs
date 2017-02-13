using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCartTests.Tests
{
    public class Case12AddProduct : BaseTest
    {
        [SetUp]
        public void Login()
        {
            Scenarios.LoginToAdminZone(driver);
        }

        [Test]
        public void AddProductTest()
        {
            string productName = "Test Duck " + DateTime.Now.Ticks.ToString();
            string productCategory = "Rubber Ducks";
            double productPrice = 7.99;

            driver.FindElement(By.CssSelector("[href$='?app=catalog&doc=catalog']")).Click();
            driver.FindElement(By.CssSelector("a[href$='edit_product']")).Click();

            //General tab
            driver.FindElement(By.CssSelector("[name='status'][value='1']")).Click();
            driver.FindElement(By.Name("name[en]")).SendKeys(productName);
            driver.FindElement(By.Name("code")).SendKeys("12345");
            driver.FindElement(By.CssSelector($"[data-name='{productCategory}']")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(dr => dr.FindElements(By.CssSelector("[name='default_category_id'] > option")).Count == 2);
            new SelectElement(driver.FindElement(By.Name("default_category_id"))).SelectByText(productCategory);
            driver.FindElement(By.CssSelector("[name='product_groups[]'][value='1-3']")).Click(); //Unisex
            driver.FindElement(By.Name("quantity")).Clear();
            driver.FindElement(By.Name("quantity")).SendKeys("10");
            driver.FindElement(By.Name("new_images[]")).SendKeys(ImagePath("RubberDuck.jpg"));
            driver.FindElement(By.Name("date_valid_from")).SendKeys(DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy"));
            driver.FindElement(By.Name("date_valid_to")).SendKeys(DateTime.Now.AddDays(1).ToString("MM/dd/yyyy"));

            //Information tab
            driver.FindElement(By.CssSelector(".index a[href$='#tab-information']")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#tab-information")));

            new SelectElement(driver.FindElement(By.Name("manufacturer_id"))).SelectByIndex(1);
            driver.FindElement(By.Name("keywords")).SendKeys("duck");
            driver.FindElement(By.Name("short_description[en]")).SendKeys("Duck for testing");
            driver.FindElement(By.CssSelector(".trumbowyg-editor")).SendKeys("This is description for product 'Duck for testing'");
            driver.FindElement(By.Name("head_title[en]")).SendKeys("Duck");
            driver.FindElement(By.Name("meta_description[en]")).SendKeys("meta duck");

            //Prices tab
            driver.FindElement(By.CssSelector(".index a[href$='#tab-prices']")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                .Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#tab-prices")));

            driver.FindElement(By.Name("purchase_price")).Clear();
            driver.FindElement(By.Name("purchase_price")).SendKeys((productPrice - 2).ToString(CultureInfo.InvariantCulture));
            new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code"))).SelectByValue("USD");
            driver.FindElement(By.Name("prices[USD]")).SendKeys(productPrice.ToString(CultureInfo.InvariantCulture));
            driver.FindElement(By.Name("prices[EUR]")).SendKeys((productPrice * 1.2).ToString(CultureInfo.InvariantCulture));

            //Save
            driver.FindElement(By.Name("save")).Click();

            //Check product in admin catalog: check that last line contains product name
            var createdProductName = driver.FindElements(By.CssSelector(".dataTable .row > td:nth-of-type(3)")).Last().Text;
            Assert.That(createdProductName, Is.EqualTo(productName));
        }

        private string ImagePath(string image)
        {
            string testDir = TestContext.CurrentContext.TestDirectory;
            return System.IO.Path.Combine(testDir, "Images", image);
        }
    }
}
