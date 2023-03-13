using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecFlowProject.POMClasses;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace SpecFlowProject.Support
{


    [Binding]
    internal class Hooks
    {
        public static IWebDriver driver;
        protected string baseUrl;
        protected string browser = "chrome";

        [Before]
        public void SetUp()
        {
            browser = TestContext.Parameters["browser"];
            Console.WriteLine("Read in browser var from command line: " + browser);
            // browser = browser.ToLower().Trim(); //works in cmd but not right click run in vs
            switch (browser)
            {
                case "chrome":
                    driver = new ChromeDriver(); break;
                case "firefox":
                    driver = new FirefoxDriver(); break;
                default:
                    Console.WriteLine("Unknown browser, starting chrome");
                    driver = new ChromeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();
            driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/"; //opens the site

            LoginPagePOM loginPage = new LoginPagePOM(driver);

            loginPage.SetUsername("pritesh.panchal@nfocus.co.uk");
            string login_password = Environment.GetEnvironmentVariable("loginpassword"); //Using an environment variable to get the password for security and data protection
            var passwordField = driver.FindElement(By.Id("password"));
            driver.FindElement(By.Id("password")).SendKeys(login_password);
            loginPage.ClickLogin();
   

            MyAccountPagePOM goToShopPage = new MyAccountPagePOM(driver); //This block of code directs you to shop page and clears any overlapping elements
            goToShopPage.GoToShop();

            ShopPagePOM shopPage = new ShopPagePOM(driver);
            shopPage.DismissNotice();

            IWebElement randomAddToCartLink = shopPage.SelectRandomAddToCartLink(); //uses a random generator to select an item at random
            randomAddToCartLink.Click();
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(drv => drv.FindElement(By.LinkText("View cart")).Displayed);
            shopPage.ViewCart();
            
        }

        [After]
        public void TearDown() //After test actions
        {
            HomePagePOM homePage = new HomePagePOM(driver);
            homePage.MyAccountPage();

            MyAccountPagePOM logOut = new MyAccountPagePOM(driver);
            logOut.Logout();
            // driver.Quit();
        }
    }
}
