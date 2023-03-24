using OpenQA.Selenium;
using SpecFlowProject.Support;
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

        IWebElement _addToCart => _driver.FindElement(By.LinkText("Add to cart"));
        IWebElement _viewCart => _driver.FindElement(By.LinkText("View cart"));
        IList<IWebElement> _addToCartLinks => _driver.FindElements(By.LinkText("Add to cart"));


        /*This is a random generator that selects items at random to be added to cart and then finds the sibling element so the name of the
          item can be logged*/
        public string SelectRandomAddToCartLink()
        {

            Random random = new Random();
            IWebElement randomAddToCartLink = _addToCartLinks[random.Next(_addToCartLinks.Count)];  //Randomly selects an item  
            IWebElement liElement = randomAddToCartLink.FindElement(By.XPath("..")); //Finds the parent element to locate item name
            IWebElement h2Element = liElement.FindElement(By.TagName("h2")); //finds the item name which is a h2 tag
            string item = h2Element.Text;  //stores item name in variable to be called in step definition file and then logged
            randomAddToCartLink.Click();
            return item;
        }

        public void AddToCart()
        {
            _addToCart.Click();
        }

        public void ViewCart()
        {
            HelperMethods.WaitForElement(By.LinkText("View cart"), 3, _driver);
            _viewCart.Click();
        }

    }
}
