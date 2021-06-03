using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lec_6
{
    [TestFixture]
    public class UnitTest1
    {

        IWebDriver driver;

        [SetUp]
        public void BeforeTest()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Task2()
        {
            string url = "https://demoqa.com/automation-practice-form";
            driver.Navigate().GoToUrl(url);
            var Subject = new[] { "Math","Chemistry", "Physics" };
            var userIds = 0;
            var testUsers = new Faker<User>()
                .RuleFor(o => o.Subject, f => f.PickRandom(Subject))
                .RuleFor(o => o.Phone, f => f.Person.Phone)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Adress, f => f.Lorem.Sentence())
                .RuleFor(o => o.EmailAddress, (f, u) => f.Internet.Email(u.FirstName, u.LastName));
            var user = testUsers.Generate();

            IWebElement FirstnameField = driver.FindElement(By.CssSelector("input[id='firstName']"));
            FirstnameField.Click();
            FirstnameField.Clear();
            FirstnameField.SendKeys(user.FirstName);

            IWebElement LastnameField = driver.FindElement(By.CssSelector("input[id='lastName']"));
            LastnameField.Click();
            LastnameField.Clear();
            LastnameField.SendKeys(user.LastName);

            IWebElement EmailField = driver.FindElement(By.CssSelector("input[id='userEmail']"));
            EmailField.Click();
            EmailField.Clear();
            EmailField.SendKeys(user.EmailAddress);

            IWebElement PhoneField = driver.FindElement(By.CssSelector("input[id='userNumber']"));
            PhoneField.Click();
            PhoneField.Clear();
            PhoneField.SendKeys(user.Phone);

            var Radios = driver.FindElements(By.CssSelector(".custom-radio")).ToList();
            Radios.ElementAt(new Random().Next(0, Radios.Count - 1)).Click();

            var Checkbox = driver.FindElements(By.CssSelector(".custom-checkbox")).ToList();
            Checkbox.ElementAt(new Random().Next(0, Checkbox.Count - 1)).Click();


            /*IWebElement SubjectField = driver.FindElement(By.CssSelector("input[id='subjectsInput']"));
            SubjectField.Click();
            SubjectField.Clear();
            SubjectField.SendKeys(user.Subject);
            Thread.Sleep(2000);
            IWebElement SubjectBtn = driver.FindElement(By.XPath("//*[contains(text(),'" + user.Subject + "')]"));
            SubjectBtn.Click();
*/
            IWebElement SubmitBtn = driver.FindElement(By.CssSelector("#userForm"));
            SubmitBtn.Submit();
        }

        [TearDown]
        public void AfterTest()
        {
            //driver.Quit();
        }
    }
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string Subject { get; set; }

    }
}
