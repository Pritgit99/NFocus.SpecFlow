using OpenQA.Selenium;
using SpecFlowProject.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpecFlowProject.Support.HelperMethods;

namespace SpecFlowProject.POMClasses
{
    internal class CartPagePOM
    {
        private IWebDriver _driver; //field to hold webdriver instance


        public CartPagePOM(IWebDriver driver) //get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators
        IWebElement _subtotal => _driver.FindElement(By.CssSelector("div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span"));
        IWebElement _currentDiscount => _driver.FindElement(By.CssSelector("div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > td > span"));
        IWebElement _shippingAmount => _driver.FindElement(By.CssSelector("#shipping_method > li > label > span"));
        IWebElement _couponField => _driver.FindElement(By.Id("coupon_code"));
        IWebElement _applyCoupon => _driver.FindElement(By.Name("apply_coupon"));
        IWebElement _goToCheckout => _driver.FindElement(By.LinkText("Proceed to checkout"));
        IWebElement _cartItem => _driver.FindElement(By.CssSelector("form > table > tbody > tr.woocommerce-cart-form__cart-item.cart_item"));
        IWebElement _clearCart => _driver.FindElement(By.CssSelector(".woocommerce-cart-form__cart-item.cart_item > td.product-remove > a"));
        IWebElement _cartTable => _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table"));



        //Scrolls into the payment table to take a screenshot
        public void ScrollCartTableIntoView()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", _cartTable);
            HelperMethods.WaitForElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table"), 10, _driver);
        }

        //Clears the cart once a test is complete
        public void ClearCart()
        {
            IReadOnlyCollection<IWebElement> clearItems = _driver.FindElements(By.ClassName("remove"));
            foreach (IWebElement item in clearItems)
            {
                IWebElement removeButton = _driver.FindElement(By.ClassName("remove"));  
                removeButton.Click();
                Thread.Sleep(1000);
            }
         }

        //Gets the cart table to check if there is an item displayed
        public IWebElement GetCartItem()
        {
            return _cartItem;
        }

        //Captures the table of the payment summary so a screenshot can be taken
        public IWebElement GetCartTable()
        {
            return _cartTable;
        }

        //Finds the coupon field and sends the coupon value to it
        public void SetCoupon(string couponCode) //Clicks on the coupon field and applies the discount
        {
            _couponField.Clear();
            _couponField.Click();
            _couponField.SendKeys(couponCode);
        }

        //Clicks the apply coupon button
        public void ApplyCoupon()
        {
            _applyCoupon.Click();
        }

        //Clicks the proceed to checkout button
        public void ProceedToCheckout()
        {
            _goToCheckout.Click();
        }

        //Captures the subtotal amount
        public decimal GetSubtotal() //Gets the subtotal value on the website and stores it as a double
        {
            
            string newSubtotal = _subtotal.Text;
            string newsubtotal = newSubtotal[1..];
            decimal finalsubtotal = decimal.Parse(newsubtotal);
            return finalsubtotal;
        }

        //Captures the discount amount
        public decimal GetCurrentDiscount() //Gets the current discount figure and stores it as a double
        {
            
            string coupondiscount = _currentDiscount.Text; 
            string newcoupon = coupondiscount[1..];
            decimal finalcoupon = decimal.Parse(newcoupon);
            return finalcoupon;
        }

        //Captures the shipping amount
        public decimal GetShippingAmount()
        {
            string shippingAmount = _shippingAmount.Text;
            string newShipping = shippingAmount[1..];
            decimal finalShipping = decimal.Parse(newShipping);
            return finalShipping;
        }

    }
}