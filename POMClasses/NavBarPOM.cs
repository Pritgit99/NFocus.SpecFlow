using OpenQA.Selenium;
using SpecFlowProject.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.POMClasses
{
    internal class NavBarPOM //This class controls the actions of the account page in both logged in and logged out states
    {
        private IWebDriver _driver; //field to hold webdriver instance

        public NavBarPOM(IWebDriver driver) //get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators
        IWebElement _shopLink => _driver.FindElement(By.LinkText("Shop"));
        IWebElement _logOutLink => _driver.FindElement(By.LinkText("Logout"));
        IWebElement _goToOrderPage => _driver.FindElement(By.LinkText("Orders"));
        IWebElement _myaccountLink => _driver.FindElement(By.LinkText("My account"));
        IWebElement _cartLink => _driver.FindElement(By.LinkText("Cart"));
        IWebElement _dismissNotice => _driver.FindElement(By.ClassName("woocommerce-store-notice__dismiss-link"));


        //This POM class handles the navigation of the website and any popups/javascript.
        public void DismissNotice()
        {
            _dismissNotice.Click();
        }

        //service method
        public void GoToShop()
        {
            _shopLink.Click();

        }

        public void GoToCart()
        {
            _cartLink.Click();

        }

        //service method
        public void MyAccountPage()
        {
            HelperMethods.WaitForElement(By.LinkText("My account"), 3, _driver);
            _myaccountLink.Click();

        }

        public void GoToOrder()
        {
            _goToOrderPage.Click();
        }

        public void Logout()
        {
            _logOutLink.Click(); 
        }

    }

}
