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
            string productName = "Test Duck";
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

            driver.FindElement(By.Name("new_images[]")).SendKeys(ImagesPath("RubberDuck.jpg"));
        }

        private string ImagesPath(string image)
        {
            string testDir = TestContext.CurrentContext.TestDirectory;
            return System.IO.Path.Combine(testDir, "Images", image);
        }
    }
}
