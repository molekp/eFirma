using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Project.Tests.Selenium
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var driver = new FirefoxDriver();
            string baseURL = "http://seleniumhq.org/";
            driver.Navigate().GoToUrl(baseURL);
            driver.FindElement(By.LinkText("Projects")).Click();
            driver.FindElement(By.LinkText("Selenium IDE")).Click();
            Assert.AreEqual(driver.FindElement(By.XPath("//div[@id='mainContent']/table/tbody/tr/td/p/b")).Text, "Selenium IDE");
            driver.Close();
        }
    }
}
