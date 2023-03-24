using OpenQA.Selenium;
using SpecFlowProject.Support;
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

        IWebElement _firstNameField => _driver.FindElement(By.Id("billing_first_name")); //These IWebElements finds the designated fields/elements
        IWebElement _lastNameField => _driver.FindElement(By.Id("billing_last_name"));
        IWebElement _companyNameField => _driver.FindElement(By.Id("billing_company"));
        IWebElement _addressField1 => _driver.FindElement(By.Id("billing_address_1"));
        IWebElement _addressField2 => _driver.FindElement(By.Id("billing_address_2"));
        IWebElement _cityField => _driver.FindElement(By.Id("billing_city"));
        IWebElement _countyField => _driver.FindElement(By.Id("billing_state"));
        IWebElement _postcodeField => _driver.FindElement(By.Id("billing_postcode"));
        IWebElement _phoneNumberField => _driver.FindElement(By.Id("billing_phone"));
        IWebElement _emailField => _driver.FindElement(By.Id("billing_email"));
        IWebElement _differentShippingField => _driver.FindElement(By.Name("ship_to_different_address"));
        IWebElement _orderCommentsField => _driver.FindElement(By.Id("order_comments"));
        IWebElement _checkPaymentField => _driver.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque > label"));
        IWebElement _placeOrderButton => _driver.FindElement(By.Name("woocommerce_checkout_place_order"));



        //This POM class fills out the checkout page by sending values from the runsettings parameter and then places an order.
        public void SetFirstName(string firstName) //Clears the first name field and sets the value
        {
            _firstNameField.Clear();
            _firstNameField.Click();
            _firstNameField.SendKeys(firstName);
        }

        public void SetLastName(string lastName)
        {
            _lastNameField.Clear();
            _lastNameField.Click();
            _lastNameField.SendKeys(lastName);
        }

        public void SetCompanyName(string companyName)
        {
            _companyNameField.Clear();
            _companyNameField.Click();
            _companyNameField.SendKeys(companyName);
        }

        public void SetAddressLine1(string addressLine1)
        {
            _addressField1.Clear();
            _addressField1.Click();
            _addressField1.SendKeys(addressLine1);
        }


        public void SetAddressLine2(string addressLine2)
        {
            _addressField2.Clear();
            _addressField2.Click();
            _addressField2.SendKeys(addressLine2);
        }


        public void SetBillingCity(string city)
        {
            _cityField.Clear();
            _cityField.Click();
            _cityField.SendKeys(city);
        }

        public void SetCounty(string county)
        {
            _countyField.Clear();
            _countyField.Click();
            _countyField.SendKeys(county);
        }

        public void SetPostcode(string postcode)
        {
            _postcodeField.Clear();
            _postcodeField.Click();
            _postcodeField.SendKeys(postcode);
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            _phoneNumberField.Clear();
            _phoneNumberField.Click();
            _phoneNumberField.SendKeys(phoneNumber);
        }

        public void SetEmail(string emailAddress)
        {
            _emailField.Clear();
            _emailField.Click();
            _emailField.SendKeys(emailAddress);
        }

        public void ClickDifferentShippingAddress()
        {
            _differentShippingField.Click();
        }

        public void SetOrderComents(string order_comments)
        {
            _orderCommentsField.Clear();
            _orderCommentsField.SendKeys(order_comments);
        }

        public void CheckPayment()
        {
            _checkPaymentField.Click();
            HelperMethods.WaitForElement(By.Name("woocommerce_checkout_place_order"), 6, _driver);

        }

        public void ClickPlaceOrder()
        {
            _placeOrderButton.Click();
        }

    }
}
