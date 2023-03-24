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
        IWebElement _logOut => _driver.FindElement(By.LinkText("Logout"));
        IWebElement _pastOrders => _driver.FindElement(By.CssSelector("div > table > tbody > tr:nth-child(1) > td.woocommerce-orders-table__cell.woocommerce-orders-table__cell-order-number > a"));
        IWebElement _ordersTable => _driver.FindElement(By.CssSelector("#post-7 > div > div > div > table"));


        public void Logout()
        {
            _logOut.Click();
        }

        //Finds the order history tables first row and removes the first character of the order number so it can be used in an assertion
        public string GetOrders()
        {
            string orderHistory = _pastOrders.Text;
            if (orderHistory.StartsWith("#"))
            {
                orderHistory = orderHistory[1..];
            }
            return orderHistory;
        }


        //Used to grab the order table as a whole
        public IWebElement GetOrdersTable()
        {
            return _ordersTable;
        }

    }
}

