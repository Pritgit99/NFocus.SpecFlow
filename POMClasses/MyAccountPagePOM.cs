using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.POMClasses
{
    internal class MyAccountPagePOM //This class controls the actions of the account page in both logged in and logged out states
    {
        private IWebDriver _driver; //field to hold webdriver instance

        public MyAccountPagePOM(IWebDriver driver) //get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators
        IWebElement shopLink => _driver.FindElement(By.LinkText("Shop"));
        IWebElement logOutLink => _driver.FindElement(By.LinkText("Logout"));
        IWebElement goToOrderPage => _driver.FindElement(By.LinkText("Orders"));


        //service method
        public void GoToShop()
        {
            shopLink.Click();

        }

        public void Logout()
        {
            logOutLink.Click(); 
        }

        public void GoToOrder()
        {
            goToOrderPage.Click();
        }

    }

}
