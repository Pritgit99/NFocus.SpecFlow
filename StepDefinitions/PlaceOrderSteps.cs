using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowProject.POMClasses;
using System;
using TechTalk.SpecFlow;
using static SpecFlowProject.Support.HelperMethods;
using SpecFlowProject.Support;
using OpenQA.Selenium.Support.UI;
using System.Data;

namespace SpecFlowProject.Features
{
    [Binding]
    public class StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver driver;

        public StepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this.driver = (IWebDriver)_scenarioContext["driver"];
        }

        [Given(@"i am logged in on the shop page")]
        public void GivenIAmLoggedInOnTheShopPage()
        {
            LoginPagePOM loginPage = new LoginPagePOM(driver);
            loginPage.SetUsername("pritesh.panchal@nfocus.co.uk");
            string login_password = Environment.GetEnvironmentVariable("loginpassword"); //Using an environment variable to get the password for security and data protection
            loginPage.SetPassword(login_password);
            loginPage.ClickLogin();

            NavBarPOM goToShopPage = new NavBarPOM(driver); //This block of code directs you to shop page and clears any overlapping elements
            goToShopPage.GoToShop();
        }

        [When(@"I add an item to my cart")]
        public void WhenIAddAnItemToMyCart()
        {
            ShopPagePOM shopPage = new ShopPagePOM(driver);
            string itemName = shopPage.SelectRandomAddToCartLink(); //Calls the POM to select an item at random
            Console.WriteLine("The selected item is: " + itemName); //Logs the item selected
        }


        [When(@"I apply the coupon code ""(.*)""")]
        public void WhenIApplyTheCouponCode(string couponCode)  //couponCode is passed through via the BDD as a parameter incase of changes
        {
            ShopPagePOM shopPage = new ShopPagePOM(driver);
            shopPage.ViewCart();                                //Directs to view cart page

            var cartPagePOM = new CartPagePOM(driver);
            Assert.IsTrue(cartPagePOM.GetCartItem().Displayed); //Checks if there actually is an item in the cart
            CartPagePOM cartPage = new CartPagePOM(driver);
            cartPage.SetCoupon(couponCode);                 //Sets the coupon value as edgewords (fetched from feature file)
            cartPage.ApplyCoupon();
        }

        [Then(@"a discount of (\d+)% should be applied to my basket total")]
        public void ThenADiscountOfShouldBeAppliedToMyBasketTotal(int discountPercentage)
        {

            try //Try method to check if the current discount on the website is the correct one and if not then the ssertion is caught 
            {
                CartPagePOM cartPage = new CartPagePOM(driver);
                WaitFor(() => cartPage.GetCurrentDiscount() != 0, 3, driver);   //Waits for discount to apply

                // Get the cart price values as decimals
                decimal subtotal = cartPage.GetSubtotal();
                decimal couponDiscount = cartPage.GetCurrentDiscount();
                decimal expectedDiscount = subtotal * discountPercentage/100;
                decimal shippingAmount = cartPage.GetShippingAmount();

                //Calculates the total price and expected total price of the order
                decimal expectedTotal = subtotal - expectedDiscount + shippingAmount;
                decimal actualTotal = subtotal - couponDiscount + shippingAmount;

                // Check that the coupon discount matches the expected discount
                Assert.AreEqual(expectedDiscount, couponDiscount);
                Assert.AreEqual(expectedTotal, actualTotal);

                //Outputs for test purposes
                Console.WriteLine("Test complete, the discount is correct");
                Console.WriteLine("The current discount is £" + couponDiscount);
                Console.WriteLine("The expected discount is £" + expectedDiscount);
                Console.WriteLine("The expected total is £" + expectedTotal);
                Console.WriteLine("The actual total is £" + actualTotal);
                

            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Test failed. The discount is incorrect: {ex.Message}");

                //Scroll so a screenshot can be taken
                CartPagePOM cartPage = new CartPagePOM(driver);
                cartPage.ScrollCartTableIntoView();
               
                //take screenshot and report
                IWebElement cartTable = cartPage.GetCartTable();
                HelperMethods.TakeScreenshotOfElement(driver, cartTable, "Incorrect_discount.png");
            }
            
        }

        [When(@"I go through checkout")]
        public void WhenIGoThroughCheckout()
        {
            ShopPagePOM shopPage = new ShopPagePOM(driver);
            shopPage.ViewCart();

            var cartPagePOM = new CartPagePOM(driver);
            Assert.IsTrue(cartPagePOM.GetCartItem().Displayed);

            CartPagePOM goToCheckout = new CartPagePOM(driver);
            goToCheckout.ProceedToCheckout();

            //Fetched from runsettings and takes into account for null values
            string firstName = GetParameterValue("firstNameField");
            string lastName = GetParameterValue("lastNameField");
            string companyName = GetParameterValue("companyNameField");
            string addressLine1 = GetParameterValue("addressField1");
            string addressLine2 = GetParameterValue("addressField2");
            string city = GetParameterValue("cityField");
            string county = GetParameterValue("countyField");
            string postcode = GetParameterValue("postcodeField");
            string phoneNumber = GetParameterValue("phoneNumberField");
            string emailAddress = GetParameterValue("emailField");

            //Sets the billing fields based on runsettings
            CheckoutPagePOM orderDetails = new CheckoutPagePOM(driver);
            orderDetails.SetFirstName(firstName);
            orderDetails.SetLastName(lastName);
            orderDetails.SetCompanyName(companyName);
            orderDetails.SetAddressLine1(addressLine1);
            orderDetails.SetAddressLine2(addressLine2);
            orderDetails.SetBillingCity(city);
            orderDetails.SetCounty(county);
            orderDetails.SetPostcode(postcode);
            orderDetails.SetPhoneNumber(phoneNumber);
            orderDetails.SetEmail(emailAddress);
            orderDetails.SetOrderComents("Test case 2");
            orderDetails.CheckPayment();
            orderDetails.ClickPlaceOrder();
        }

        [Then(@"the order number shown should be on the order history page")]
        public void ThenTheOrderNumberShownShouldBeOnTheOrderHistoryPage()
        {
            //Waits for the order confirmation number to be valid before continuing
            OrderConfirmedPagePOM orderConfirmationPage = new OrderConfirmedPagePOM(driver);
            WaitFor(() => orderConfirmationPage.GetOrderNumber() != null, 3, driver);


            //Obtains the order number and stores it in a variable
            string orderNumber = orderConfirmationPage.GetOrderNumber();

            //Goes to order history page to check if the order number is there
            OrderConfirmedPagePOM goToMyAccount = new OrderConfirmedPagePOM(driver);
            goToMyAccount.AccountPage();

            NavBarPOM goToOrder = new NavBarPOM(driver);
            goToOrder.GoToOrder();

            try //Try-catch method to check if the order number on the confirmation page is the same as the order history
            {
                OrdersPagePOM ordersPage = new OrdersPagePOM(driver);
                string pastOrders = ordersPage.GetOrders();
                Assert.AreEqual(orderNumber, pastOrders);

                Console.WriteLine("Test complete. Order number on orders page matches the number captured on order confirmation page");
                Console.WriteLine("The order confirmation number was " + orderNumber);
                Console.WriteLine("The orders page number was " + pastOrders);

            }
            catch (Exception e)
            {
                Console.WriteLine("Test failed. Order numbers do not match " + e.Message);


                OrdersPagePOM ordersHistroy = new OrdersPagePOM(driver);
                IWebElement orderTable = ordersHistroy.GetOrdersTable();
                HelperMethods.TakeScreenshotOfElement(driver, orderTable, "OrderNumberNotDisplayed.png");
            }
        }

    }
}