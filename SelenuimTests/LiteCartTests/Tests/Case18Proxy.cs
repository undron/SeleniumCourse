using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCartTests.Tests
{
    class Case18Proxy
    {
        IWebDriver driver;

        [SetUp]
        public void CreateDriver()
        {
            var proxy = new Proxy();
            proxy.Kind = ProxyKind.Manual;
            proxy.HttpProxy = "127.0.0.1:8888";
            var options = new ChromeOptions();
            options.Proxy = proxy;
            driver = new ChromeDriver(options);
        }

        [Test]
        public void ProxyTest()
        {
            Scenarios.LoginToAdminZone(driver);
            Assert.That(driver.Title, Is.EqualTo("My Store"));
        }

        [TearDown]
        public void QuitDriver()
        {
            driver?.Quit();
        }
    }
}
