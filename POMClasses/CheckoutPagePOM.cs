using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.POMClasses
{
    internal class CheckoutPagePOM
    {
        private IWebDriver _driver;
        public CheckoutPagePOM(IWebDriver driver) //get the webdriver instance from the calling test
        {
            this._driver = driver;
        }

        //Locators

        IWebElement firstNameField => _driver.FindElement(By.Id("billing_first_name")); //These IWebElements finds the designated fields/elements
        IWebElement lastNameField => _driver.FindElement(By.Id("billing_last_name"));
        IWebElement companyNameField => _driver.FindElement(By.Id("billing_company"));
        IWebElement addressField1 => _driver.FindElement(By.Id("billing_address_1"));
        IWebElement addressField2 => _driver.FindElement(By.Id("billing_address_2"));
        IWebElement cityField => _driver.FindElement(By.Id("billing_city"));
        IWebElement countyField => _driver.FindElement(By.Id("billing_state"));
        IWebElement postcodeField => _driver.FindElement(By.Id("billing_postcode"));
        IWebElement phoneNumberField => _driver.FindElement(By.Id("billing_phone"));
        IWebElement emailField => _driver.FindElement(By.Id("billing_email"));
        IWebElement differentShippingField => _driver.FindElement(By.Name("ship_to_different_address"));
        IWebElement orderCommentsField => _driver.FindElement(By.Id("order_comments"));
        IWebElement checkPaymentField => _driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque > label"));
        IWebElement placeOrderButton => _driver.FindElement(By.Name("woocommerce_checkout_place_order"));



        //service method


        public void SetFirstName(string firstName) //Clears the first name field and sets the value
        {
            firstNameField.Clear();
            firstNameField.Click();
            firstNameField.SendKeys(firstName);
        }

        public void SetLastName(string lastName)
        {
            lastNameField.Clear();
            lastNameField.Click();
            lastNameField.SendKeys(lastName);
        }

        public void SetCompanyName(string companyName)
        {
            companyNameField.Clear();
            companyNameField.Click();
            companyNameField.SendKeys(companyName);
        }

        public void SetAddressLine1(string addressLine1)
        {
            addressField1.Clear();
            addressField1.Click();
            addressField1.SendKeys(addressLine1);
        }


        public void SetAddressLine2(string addressLine2)
        {
            addressField2.Clear();
            addressField2.Click();
            addressField2.SendKeys(addressLine2);
        }


        public void SetBillingCity(string city)
        {
            cityField.Clear();
            cityField.Click();
            cityField.SendKeys(city);
        }

        public void SetCounty(string county)
        {
            countyField.Clear();
            countyField.Click();
            countyField.SendKeys(county);
        }

        public void SetPostcode(string postcode)
        {
            postcodeField.Clear();
            postcodeField.Click();
            postcodeField.SendKeys(postcode);
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            phoneNumberField.Clear();
            phoneNumberField.Click();
            phoneNumberField.SendKeys(phoneNumber);
        }

        public void SetEmail(string emailAddress)
        {
            emailField.Clear();
            emailField.Click();
            emailField.SendKeys(emailAddress);
        }

        public void ClickDifferentShippingAddress()
        {
            differentShippingField.Click();
        }

        public void SetOrderComents(string order_comments)
        {
            orderCommentsField.Clear();
            orderCommentsField.SendKeys(order_comments);
        }

        public void CheckPayment()
        {
            checkPaymentField.Click();
        }

        public void ClickPlaceOrder()
        {
            placeOrderButton.Click();
        }

    }
}
