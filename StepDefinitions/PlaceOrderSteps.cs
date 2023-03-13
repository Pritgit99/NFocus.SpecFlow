using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowProject.POMClasses;
using System;
using TechTalk.SpecFlow;
using static SpecFlowProject.Support.Hooks;
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
        

        CartPagePOM cartPage = new CartPagePOM(driver);
        CartPagePOM goToCheckout = new CartPagePOM(driver);
        OrderConfirmedPagePOM orderConfirmationPage = new OrderConfirmedPagePOM(driver);
        OrderConfirmedPagePOM goToMyAccount = new OrderConfirmedPagePOM(driver);
        MyAccountPagePOM goToOrder = new MyAccountPagePOM(driver);
        OrdersPagePOM ordersPage = new OrdersPagePOM(driver);
        CheckoutPagePOM orderDetails = new CheckoutPagePOM(driver); //Fill out personal details for order

        public StepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I am on the cart page with an item in my basket")]
        public void GivenIAmOnTheCartPageWithAnItemInMyBasket()
        {
            
            var item = driver.FindElement(By.CssSelector("#post-5 > div > div > form > table > tbody > tr.woocommerce-cart-form__cart-item.cart_item"));
            Assert.IsTrue(item.Displayed);
            
        }

        [When(@"I enter edgewords")]
        public void WhenIEnterEdgewords()
        {

            cartPage.SetCoupon("edgewords"); //Sets the coupon value as edgewords
        }

        [When(@"I click Apply")]
        public void WhenIClickApply()
        {
            cartPage.ApplyCoupon();

        }

        [Then(@"There should be a (.*)% discount")]
        public void ThenThereShouldBeADiscount(int p0)
        {
            
            try //Try method to check if the current discount on the website is the correct one and if not then the ssertion is caught 
            {

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                wait.Until(drv => cartPage.GetCurrentDiscount() != 0);

                // Verify the coupon discount
                decimal subtotal = cartPage.GetSubtotal();
                decimal couponDiscount = cartPage.GetCurrentDiscount();

                // Calculate the expected discount and round to 2 decimal places
                decimal expectedDiscount = Math.Round(subtotal * 0.15m, 2);

                // Check that the coupon discount matches the expected discount
                Assert.AreEqual(expectedDiscount, couponDiscount);
                Console.WriteLine("Test complete, the discount is correct");
                Console.WriteLine("current discount is " + couponDiscount);
                Console.WriteLine("The expected discount is " + expectedDiscount);

            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Test failed. The discount is incorrect: {ex.Message}");


                //Scroll so a screenshot can be taken
                IWebElement element = driver.FindElement(By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table"));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView();", element);
                Thread.Sleep(2000);

                //take screenshot and report
                HelperMethods.TakeScreenshotOfElement(driver, By.CssSelector("#post-5 > div > div > div.cart-collaterals > div > table"), "Incorrect_discount.png");
            }
        }


        [When(@"I click proceed to checkout")]
        public void WhenIClickProceedToCheckout()
        {
            goToCheckout.ProceedToCheckout();
        }

        [When(@"I place order")]
        public void WhenIPlaceOrder()
        {
            
            //Fetched from runsettings
            string firstName = TestContext.Parameters["firstNameField"];
            string lastName = TestContext.Parameters["lastNameField"];
            string companyName = TestContext.Parameters["companyNameField"];
            string addressLine1 = TestContext.Parameters["addressField1"];
            string addressLine2 = TestContext.Parameters["addressField2"];
            string city = TestContext.Parameters["cityField"];
            string county = TestContext.Parameters["countyField"];
            string postcode = TestContext.Parameters["postcodeField"];
            string phoneNumber = TestContext.Parameters["phoneNumberField"];
            string emailAddress = TestContext.Parameters["emailField"];

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
            orderDetails.ClickDifferentShippingAddress();
            orderDetails.ClickDifferentShippingAddress();
            orderDetails.SetOrderComents("This is a test");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(drv => drv.FindElement(By.CssSelector("#payment > ul > li.wc_payment_method.payment_method_cheque > label")).Displayed);

            orderDetails.CheckPayment();

            wait.Until(drv => drv.FindElement(By.Name("woocommerce_checkout_place_order")).Enabled);

            orderDetails.ClickPlaceOrder();

        }

        [Then(@"The order number displayed should also be on the orders page")]
        public void ThenTheOrderNumberDisplayedShouldAlsoBeOnTheOrdersPage()
        {

            try //Try-catch method to check if the order number on the confirmation page is the same as the order history
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                wait.Until(drv => orderConfirmationPage.GetOrderNumber());

                //Obtains the order number and stores it in a variable
                string orderNumber = orderConfirmationPage.GetOrderNumber();

                goToMyAccount.AccountPage();
                goToOrder.GoToOrder();
                string pastOrders = ordersPage.GetOrders();
                Assert.AreEqual(orderNumber, pastOrders);

                Console.WriteLine("Test complete. Order number on orders page matches the number captured on order confirmation page");
                Console.WriteLine("The order confirmation number was " + orderNumber);
                Console.WriteLine("The orders page number was " + pastOrders);

            }
            catch (Exception e)
            {
                Console.WriteLine("Test failed. Order numbers do not match " + e.Message);

                //take screenshot and report
                HelperMethods.TakeScreenshotOfElement(driver, By.CssSelector("#post-7 > div > div > div > table"), "order_Number_Mismatch.png");

            }
        }
    }
}