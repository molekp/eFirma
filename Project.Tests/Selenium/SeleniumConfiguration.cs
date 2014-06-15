using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Selenium;
using Class = Project.Tests.Selenium.Login;

namespace Project.Tests.Selenium
{
    public class SeleniumConfiguration
    {

        public const string BaseUrl = "http://localhost:65062";
        private const string VirtualPath = "";

        private const int TimeOut = 30;
        private static int _testClassesRunning;

        private static readonly IWebDriver StaticDriver = CreateDriverInstance();
        private ISelenium m_selenium = CreateSeleniumInstance(StaticDriver,BaseUrl);

        private static Class.Login _currentlyLoggedInAs;

        static SeleniumConfiguration()
        {
            StaticDriver.Manage().Timeouts().ImplicitlyWait(
               TimeSpan.FromSeconds(TimeOut));
        }

        // Pass in null if want to run your test-case without logging in.
        public static void ClassInitialize(Class.Login login = null)
        {
            _testClassesRunning++;
            if (login == null)
            {
                //Logoff();
            }
            else if (!IsCurrentlyLoggedInAs(login))
            {
                Logon(login);
            }
        }

        public static void ClassCleanup()
        {
            try
            {
                _testClassesRunning--;
                if (_testClassesRunning == 0)
                {
                    StaticDriver.Quit();
                }
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public IWebDriver Driver
        {
            get { return StaticDriver; }
        }

        public ISelenium SeleniumD
        {
            get { return m_selenium;  }
        }

        public void Open(string url)
        {
            Driver.Navigate().GoToUrl(BaseUrl + VirtualPath + url.Trim('~'));
        }

        public void Click(string id)
        {
            Click(By.Id(id));
        }

        public void Click(By locator)
        {
            Driver.FindElement(locator).Click();
        }

        public void ClickAndWait(string id, string newUrl)
        {
            ClickAndWait(By.Id(id), newUrl);
        }

        public void ClickSubmint()
        {
            Driver.FindElement(By.TagName("form")).Submit();
        }
        

        /// <summary>
        /// Use when you are navigating via a hyper-link and need for the page to fully load before 
        /// moving further.  
        /// </summary>
        public void ClickAndWait(By locator, string newUrl)
        {
            Driver.FindElement(locator).Click();
            WebDriverWait wait = new WebDriverWait(Driver,
                  TimeSpan.FromSeconds(TimeOut));
            wait.Until(d => d.Url.Contains(newUrl.Trim('~')));
        }

        public void AssertCurrentPage(string pageUrl)
        {
            var absoluteUrl = new Uri(new Uri(BaseUrl), VirtualPath +
                   pageUrl.Trim('~')).ToString();
            Assert.AreEqual(absoluteUrl, Driver.Url);
        }

        public void AssertTextContains(string id, string text)
        {
            AssertTextContains(By.Id(id), text);
        }

        public void AssertTextContains(By locator, string text)
        {
            Assert.IsTrue(Driver.FindElement(locator).Text.Contains(text));
        }

        public void AssertTextEquals(string id, string text)
        {
            AssertTextEquals(By.Id(id), text);
        }

        public void AssertTextEquals(By locator, string text)
        {
            Assert.AreEqual(text, Driver.FindElement(locator).Text);
        }

        public void AssertValueContains(string id, string text)
        {
            AssertValueContains(By.Id(id), text);
        }

        public void AssertValueContains(By locator, string text)
        {
            Assert.IsTrue(GetValue(locator).Contains(text));
        }

        public void AssertValueEquals(string id, string text)
        {
            AssertValueEquals(By.Id(id), text);
        }

        public void AssertValueEquals(By locator, string text)
        {
            Assert.AreEqual(text, GetValue(locator));
        }

        public void AssertClassEquals(string id, string text)
        {
            AssertClassEquals(By.Id(id), text);
        }

        public void AssertClassEquals(By locator, string text)
        {
            Assert.AreEqual(text, GetAttribute(locator, "class"));
        }

        public IWebElement GetElement(string id)
        {
            return Driver.FindElement(By.Id(id));
        }

        public string GetValue(By locator)
        {
            return Driver.FindElement(locator).GetAttribute("value");
        }

        public string GetAttribute(By locator, string attribute)
        {
            return Driver.FindElement(locator).GetAttribute(attribute);
        }

        public string GetText(By locator)
        {
            return Driver.FindElement(locator).Text;
        }

        public void Type(string id, string text)
        {
            var element = GetElement(id);
            element.Clear();
            element.SendKeys(text);
        }

        public void Uncheck(string id)
        {
            Uncheck(By.Id(id));
        }

        public void Uncheck(By locator)
        {
            var element = Driver.FindElement(locator);
            if (element.Selected)
                element.Click();
        }

        // Selects an element from a drop-down list.
        public void Select(string id, string valueToBeSelected)
        {
            var options = GetElement(id).FindElements(By.TagName("option"));
            foreach (var option in options)
            {
                if (valueToBeSelected == option.Text)
                    option.Click();
            }
        }

        private static IWebDriver CreateDriverInstance(string baseUrl = BaseUrl)
        {
            return new FirefoxDriver();
        }

        private static ISelenium CreateSeleniumInstance(IWebDriver driver,string baseUrl = BaseUrl)
        {
            return new WebDriverBackedSelenium(driver, baseUrl);
        }

        private static bool IsCurrentlyLoggedInAs(Class.Login login)
        {
            return _currentlyLoggedInAs != null &&
                   _currentlyLoggedInAs.Equals(login);
        }

        private static void Logon(Class.Login login)
        {
            StaticDriver.Navigate().GoToUrl(BaseUrl + VirtualPath + "/Account/Login");

            StaticDriver.FindElement(By.Id("UserName")).SendKeys(login.Username);
            StaticDriver.FindElement(By.Id("Password")).SendKeys(login.Password);
            StaticDriver.FindElement(By.TagName("form")).Submit();

            _currentlyLoggedInAs = login;
        }

        private static void Logoff()
        {
            StaticDriver.Navigate().GoToUrl(
                VirtualPath /*+ RedirectLinks.SignOff.Trim('~')*/);
            _currentlyLoggedInAs = null;
        }
    }
}
