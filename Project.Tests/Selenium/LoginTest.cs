using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using Project.Tests.Selenium.Login;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Project.Tests.Selenium
{
    [TestClass]
    public class LoginTest : SeleniumConfiguration
    {
        [ClassCleanup]
        public static void ClassCleanup()
        {
            SeleniumConfiguration.ClassCleanup();
        }

        [TestMethod]
        public void LogOnWebsiteError()
        {
            Open("~/Account/Login");

            Assert.AreEqual("Log in - My ASP.NET MVC Application", Driver.Title);

            Type("UserName", "ad");
            Type("Password", "admin");
            ClickSubmint();

            Assert.AreEqual("Log in - My ASP.NET MVC Application", Driver.Title);
        }

        [TestMethod]
        public void LogOnWebsiteSuccess()
        {
            Open("~/Account/Login");

            Assert.AreEqual("Log in - My ASP.NET MVC Application", Driver.Title);

            Type("UserName", "admin");
            Type("Password", "admin");
            ClickSubmint();

            Assert.AreEqual("Home Page - My ASP.NET MVC Application", Driver.Title);
        }

        [TestMethod]
        public void RegisterWebsiteSuccess()
        {
            Open("~/Account/Register");

            Assert.AreEqual("Register - My ASP.NET MVC Application", Driver.Title);

            Type("UserName", "nowak");
            Type("Password", "nowak");
            Type("ConfirmPassword", "nowak");
            Type("Email", "nowak@nowak.pl");
            ClickSubmint();

            Assert.AreEqual("Home Page - My ASP.NET MVC Application", Driver.Title);
        }

        [TestMethod]
        public void RegisterWebsiteError()
        {
            string a_userName ="";
            string a_password = "";
            string a_confirmPassword = "";
            string a_email = ""; 

            Open("~/Account/Register");

            Assert.AreEqual("Register - My ASP.NET MVC Application", Driver.Title);

            Type("UserName", a_userName);
            Type("Password", a_password);
            Type("ConfirmPassword", a_confirmPassword);
            Type("Email", a_email);
            ClickSubmint();

            IWebElement select = Driver.FindElement(By.ClassName("validation-summary-errors"));
            IList<IWebElement> allOptions = select.FindElements(By.TagName("li"));
            foreach (IWebElement option in allOptions)
            {
                Assert.IsTrue(option.Text.Contains("This field is required."));
            }

            Assert.AreEqual("Register - My ASP.NET MVC Application", Driver.Title);
        }

    }
}
