using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitSeleniumWebDriver
{
    public class SeleniumTests
    {
        private WebDriver driver;
     

        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        [TearDown]
        public void RearDown()
        {
            driver.Quit();
        }


        [Test]
        public void QA_Search_In_Wekipedia()
        {
            driver.Url = "https://wikipedia.org";

            driver.FindElement(By.Id("searchInput")).Click();
            driver.FindElement(By.Id("searchInput")).SendKeys("QA");
            driver.FindElement(By.Id("searchInput")).SendKeys(Keys.Enter);


            Assert.That(driver.Url, Is.EqualTo("https://en.wikipedia.org/wiki/QA"));
        }
        [Test]
        public void Calculations_With_Two_Numbers()
        {
            driver.Url = "https://sum-numbers.nakov.repl.co/";

            driver.FindElement(By.Id("number1")).Click();
            driver.FindElement(By.Id("number1")).SendKeys("6");
            driver.FindElement(By.Id("number2")).Click();
            driver.FindElement(By.Id("number2")).SendKeys("6");
            driver.FindElement(By.Id("calcButton")).Click();

            var result = driver.FindElement(By.Id("result")).Text;
            Assert.That(result, Is.EqualTo("Sum: 12"));
        }
        [Test]
        public void Calculations_With_Invailid_Numbers()
        {
            driver.Url = "https://sum-numbers.nakov.repl.co/";

            driver.FindElement(By.Id("number1")).Click();
            driver.FindElement(By.Id("number1")).SendKeys("hello");
            driver.FindElement(By.Id("number2")).Click();
            driver.FindElement(By.Id("number2")).SendKeys("6");
            driver.FindElement(By.Id("calcButton")).Click();

            var result = driver.FindElement(By.Id("result")).Text;
            Assert.That(result, Is.EqualTo("Sum: invalid input"));
        }
        [Test]
        public void Calculations_With_Reset_Function()
        {
            driver.Url = "https://sum-numbers.nakov.repl.co/";

            driver.FindElement(By.Id("number1")).Click();
            driver.FindElement(By.Id("number1")).SendKeys("6");
            driver.FindElement(By.Id("number2")).Click();
            driver.FindElement(By.Id("number2")).SendKeys("6");
            driver.FindElement(By.Id("calcButton")).Click();

            var num1 = driver.FindElement(By.Id("number1")).Text;
            Assert.That(num1, Is.Not.Null);
            var num2 = driver.FindElement(By.Id("number1")).Text;
            Assert.That(num2, Is.Not.Null);
            var result = driver.FindElement(By.Id("result")).Text;
            Assert.That(result, Is.Not.Null);


            driver.FindElement(By.Id("resetButton")).Click();

            num1 = driver.FindElement(By.Id("number1")).Text;
            Assert.That(num2, Is.EqualTo(""));
            num2 = driver.FindElement(By.Id("number2")).Text;
            Assert.That(num1, Is.EqualTo(""));
            result = driver.FindElement(By.Id("result")).Text;
            Assert.That(result, Is.EqualTo(""));


        }

        [Test]
        public void Test_Home_Page()
        {
            driver.Url = "https://shorturl.nakov.repl.co";



            var MainTitle = driver.FindElement(By.CssSelector("h1")).Text;
            

            string expectedTitle = "URL Shortener";


            Assert.That(MainTitle, Is.EqualTo(expectedTitle));
        }
        [Test]
        public void URl_Short_test_Page()
        {
            driver.Url = "https://shorturl.nakov.repl.co/urls";
            string expectedResult = "Short URLs";
            string firstCellExpectedResult = "https://nakov.com";
            string secondCellExpectedResult = "http://shorturl.nakov.repl.co/go/nak";
           
            var title = driver.Title.ToString();
            var tableCells = driver.FindElements(By.CssSelector("table tr > td"));
            
            Assert.That(title, Is.EqualTo(expectedResult));
            Assert.That(tableCells[0].Text, Is.EqualTo(firstCellExpectedResult));
            Assert.That(tableCells[1].Text, Is.EqualTo(secondCellExpectedResult));

        }

        [Test]
            public void Test_Add_Invailid_URL()
        {
            
            driver.Url = "https://shorturl.nakov.repl.co/add-url";
            driver.FindElement(By.Id("url")).SendKeys("shdfcgsugyg");
            driver.FindElement(By.CssSelector("button")).Click();

            var field = driver.FindElement(By.CssSelector(".err")).Text;
            var addfield = driver.FindElement(By.Id("url"));
            addfield.Click();
            
            

            Assert.That(field, Is.EqualTo("Invalid URL!"));

        }
        [Test]
        public void Test_Add_URL()
        {

            driver.Url = "https://shorturl.nakov.repl.co/add-url";
            var email = "https://example/";
            var time = DateTime.Now.Ticks;
            var uniqemail = email + time;
            driver.FindElement(By.Id("url")).SendKeys(uniqemail);
            driver.FindElement(By.CssSelector("button")).Click();

            var table = driver.FindElements(By.CssSelector("table tr > td"));

            foreach (var item in table)
            {
                if (item.Text.Contains(uniqemail))
                {
                    Assert.That(item.Text, Does.Contain(uniqemail));
                    break;
                }
            }
        }
        [Test]
        public void Test_Visit_URL()
        {

            driver.Url = "https://shorturl.nakov.repl.co/urls";
            

            
            var counts = driver.FindElement(By.CssSelector("tr:nth-of-type(1) > td:nth-of-type(4)")).Text;
            var countsInt = int.Parse(counts);

            var newUrl = driver.FindElement(By.CssSelector("tr:nth-of-type(1) > td:nth-of-type(2) > .shorturl"));
            newUrl.Click();

            var newUrlWindow = driver.SwitchTo().Window(driver.WindowHandles[1]);
            var expektedNewWindowTitle = "Svetlin Nakov - Svetlin Nakov – Official Web Site and Blog";

            Assert.That(newUrlWindow.Title, Is.EqualTo(expektedNewWindowTitle));

            driver.SwitchTo().Window(driver.WindowHandles[0]);

            var last_count = driver.FindElement(By.CssSelector("tr:nth-of-type(1) > td:nth-of-type(4)")).Text;
            var last_countInt = int.Parse(last_count);

            Assert.That(countsInt < last_countInt);


            
        

        }
    }
}