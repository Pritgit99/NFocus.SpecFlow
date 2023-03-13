using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.POMClasses
{
    internal class ShopPagePOM //This class allows users to add items to the cart and view them
    {
        private IWebDriver _driver;
        public ShopPagePOM(IWebDriver driver) //get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators

        IWebElement addToCart => _driver.FindElement(By.LinkText("Add to cart"));
        IWebElement viewCart => _driver.FindElement(By.LinkText("View cart"));
        IWebElement dismissNotice => _driver.FindElement(By.ClassName("woocommerce-store-notice__dismiss-link"));
        IList<IWebElement> addToCartLinks => _driver.FindElements(By.LinkText("Add to cart"));


        //service method

        public IWebElement SelectRandomAddToCartLink()
        {
            
            Random random = new Random();
            return addToCartLinks[random.Next(addToCartLinks.Count)];
        }

        public void AddToCart()
        {
            addToCart.Click();
        }

        public void ViewCart()
        {
            viewCart.Click();
        }

        public void DismissNotice()
        {
            dismissNotice.Click();
        }
    }
}
