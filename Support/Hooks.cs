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
using static SpecFlowProject.Support.HelperMethods;


namespace SpecFlowProject.Hooks
{


    [Binding]
    public class Hooks
    {
        private IWebDriver _driver;
        protected string baseUrl;
        protected string browser;
        private readonly ScenarioContext _scenarioContext;
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Before]
        public void SetUp() //Responsible for setting up the webpage and geting the browser from the runsettings file
        {
            browser = TestContext.Parameters["browser"];
            Console.WriteLine("Read in browser var from command line: " + browser);
            // browser = browser.ToLower().Trim(); //works in cmd but not right click run in vs
            switch (browser)
            {
                case "chrome":
                    _driver = new ChromeDriver(); break;
                case "firefox":
                    _driver = new FirefoxDriver(); break;
                default:
                    Console.WriteLine("Unknown browser, starting chrome");
                    _driver = new ChromeDriver();
                    break;
            }


            _driver.Manage().Window.Maximize();
            _scenarioContext["driver"] = _driver;
            _driver.Url = "https://www.edgewordstraining.co.uk/demo-site/my-account/"; //opens the site

            NavBarPOM clearNotice = new NavBarPOM(_driver);
            clearNotice.DismissNotice();

        }

        [After]
        public void TearDown() //After test actions to clear the cart and logout
        {
            NavBarPOM navBar = new NavBarPOM(_driver);
            navBar.GoToCart();
            CartPagePOM cartPage = new CartPagePOM(_driver);
            cartPage.ClearCart();

            NavBarPOM logOut = new NavBarPOM(_driver);
            logOut.MyAccountPage();
            logOut.Logout();
            _driver.Quit();
            }
        }
    }
