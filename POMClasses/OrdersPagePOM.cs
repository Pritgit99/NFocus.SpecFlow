using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.POMClasses
{
    internal class OrdersPagePOM
    {
        private IWebDriver _driver; //field to hold webdriver instance

        public OrdersPagePOM(IWebDriver driver) //get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators
        IWebElement logOut => _driver.FindElement(By.LinkText("Logout"));
        IWebElement pastOrders => _driver.FindElement(By.CssSelector("#post-7 > div > div > div > table > tbody > tr:nth-child(1) > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));


        public void Logout()
        {
            logOut.Click();
        }

        public string GetOrders()
        {
            string orderHistory = pastOrders.Text;
            if (orderHistory.StartsWith("#"))
            {
                orderHistory = orderHistory[1..];
            }
            return orderHistory;
        }

    }
}

