using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        IWebElement subtotal => _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-subtotal > td > span"));
        IWebElement currentDiscount => _driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table > tbody > tr.cart-discount.coupon-edgewords > td > span"));
        IWebElement couponField => _driver.FindElement(By.Id("coupon_code"));
        IWebElement applyCoupon => _driver.FindElement(By.Name("apply_coupon"));
        IWebElement GoToCheckout => _driver.FindElement(By.LinkText("Proceed to checkout"));


        //service method
        public void SetCoupon(string coupon_code) //Clicks on the coupon field and applies the discount
        {
            couponField.Clear();
            couponField.Click();
            couponField.SendKeys(coupon_code);
        }

        public void ApplyCoupon()
        {
            applyCoupon.Click();
        }

        public void ProceedToCheckout()
        {
            GoToCheckout.Click();
        }

        public decimal GetSubtotal() //Gets the subtotal value on the website and stores it as a double
        {
            
            string newSubtotal = subtotal.Text;
            string newsubtotal = newSubtotal[1..];
            decimal finalsubtotal = decimal.Parse(newsubtotal);
            return finalsubtotal;
        }

        public decimal GetCurrentDiscount() //Gets the current discount figure and stores it as a double
        {
            
            string coupondiscount = currentDiscount.Text; 
            string newcoupon = coupondiscount[1..];
            decimal finalcoupon = decimal.Parse(newcoupon);
            return finalcoupon;
        }

    }
}