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
    public class Case13Cart : BaseTest
    {
        [Repeat(10)]
        [Test]
        public void CartTest()
        {
            //Step 1
            driver.Navigate().GoToUrl(Scenarios.siteUrl);
            int itemsInCatr = CartQuantity();

            for (int i = 0; i < 3; i++)
            {
                //Step 2
                driver.FindElement(By.CssSelector("#box-most-popular .product:first-child")).Click();

                //Step 2.2
                HandleSelectSize();
                driver.FindElement(By.Name("add_cart_product")).Click();

                //Step 3

                //Simple solution:
                //new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(dr => CartQuantity() > itemsInCatr);
                //itemsInCatr = CartQuantity();

                //Complex solution:
                itemsInCatr = new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(CartUpdated(itemsInCatr)).itemsCount;

                //Step 4
                driver.FindElement(By.CssSelector("[title='Home']")).Click();
            }

            //Step 5
            driver.FindElement(By.PartialLinkText("Checkout")).Click();

            //Step 6
            //Stop animation
            if (driver.FindElements(By.CssSelector(".shortcuts")).Count != 0)
                driver.FindElement(By.CssSelector(".shortcuts li.shortcut:first-child")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(
                    dr => dr.FindElement(By.CssSelector("ul.items")).GetCssValue("margin-left").Contains("0px"));

            //Remove items
            int itemsToDeleteCount = driver.FindElements(By.CssSelector(".shortcuts li.shortcut")).Count();
            for (int i = 0; i < itemsToDeleteCount; i++)
            {
                var orderSummary = driver.FindElement(By.CssSelector("#order_confirmation-wrapper"));
                driver.FindElement(By.CssSelector("ul.items li.item:first-child button[name='remove_cart_item']")).Click();
                new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.StalenessOf(orderSummary));
            }
        }

        private void HandleSelectSize()
        {
            By bySelectSize = By.Name("options[Size]");
            var elements = driver.FindElements(bySelectSize);
            if (elements.Count == 1)
            {
                var selectSize = elements[0];
                new SelectElement(selectSize).SelectByIndex(2);
            }
        }

        private int CartQuantity() => Int32.Parse(driver.FindElement(By.CssSelector("#cart .quantity")).Text);

        private class Cart
        {
            public Cart(int itemsCount)
            {
                this.itemsCount = itemsCount;
            }

            public int itemsCount { get; set; }
        }

        private Func<IWebDriver, Cart> CartUpdated(int itemsCount)
        {
            return (driver) =>
            {
                int curentItemsCount = CartQuantity();
                if (curentItemsCount > itemsCount)
                    return new Cart(curentItemsCount);
                return null;
            };
        }
    }
}
