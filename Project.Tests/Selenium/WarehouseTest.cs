using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using Project.Tests.Selenium.Login;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace Project.Tests.Selenium
{
    [TestClass]
    public class WarehouseTest : SeleniumConfiguration
    {

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            ClassInitialize(Logins.UserOne);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            SeleniumConfiguration.ClassCleanup();
        }


        //addSUpply
        [TestMethod]
        public void WarehouseAddSupply()
        {
            string a_name = "test2";
            string a_date = "2013-06-12 07:10:09";

            Open("~/Supply/AddSupply");

            Assert.AreEqual("Add Supply - My ASP.NET MVC Application", Driver.Title);

            Type("Firm", a_name);
            Type("RealizationTime", a_date);
            ClickSubmint();

            IWebElement select = Driver.FindElement(By.ClassName("title"));
            IList<IWebElement> allOptions = select.FindElements(By.TagName("hgroup"));

            foreach (IWebElement option in allOptions)
            {
                Assert.IsTrue(option.Text.Contains("Supply successfully added"));
            }
        }

        [TestMethod]
        public void WarehouseAddSupplyError()
        {
            string a_name = "";
            string a_date = "2013-06-12 07:10:09";

            Open("~/Supply/AddSupply");            
            
            Assert.AreEqual("Add Supply - My ASP.NET MVC Application", Driver.Title);

            Type("Firm", a_name);
            Type("RealizationTime", a_date);
            ClickSubmint();

            IWebElement select = Driver.FindElement(By.ClassName("title"));
            IList<IWebElement> allOptions = select.FindElements(By.TagName("hgroup"));

            foreach (IWebElement option in allOptions)
            {
                Assert.IsTrue(option.Text.Contains("Adding Supply failed"));
            }
        }
    }
}
