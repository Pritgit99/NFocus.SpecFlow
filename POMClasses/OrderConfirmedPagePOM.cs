using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.POMClasses
{
    internal class OrderConfirmedPagePOM
    {
        private IWebDriver _driver; //field to hold webdriver instance

        public OrderConfirmedPagePOM(IWebDriver driver) //get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators
        IWebElement accountLink => _driver.FindElement(By.LinkText("My account"));

        IWebElement orderNumber => _driver.FindElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"));


        //service method
        public void AccountPage()
        {
            accountLink.Click();

        }

        public string GetOrderNumber() //Obtains the order number once a user places an order and stores it
        {
            
            return orderNumber.Text;
        }

    }
}
