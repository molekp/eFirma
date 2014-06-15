using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Project.Tests.Selenium.Login
{
    [TestClass]
    public class LoginTest : SeleniumConfiguration
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

        [TestMethod]
        public void CanFillAndSubmitFormAfterLogin()
        {
            Open("~/FillOutForm.aspx");

            Assert.AreEqual("Fill out form", Driver.Title);

            Type("firstName", "User");
            Type("lastName", "One");
            Type("address1", "99 Test Street");
            Type("city", "Test City");
            Type("state", "TX");

            Click("btnSubmit");

            AssertTextContains(By.CssSelector("confirm-label"), "Submission successful.");
        }
    }
}
