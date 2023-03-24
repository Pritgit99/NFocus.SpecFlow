using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.POMClasses
{
    internal class LoginPagePOM //Finds the login elements and sends the data accordingly
    {
        private IWebDriver _driver; //Field to hold a webdriver instance

        public LoginPagePOM(IWebDriver driver) //Get the webdriver instance from the calling test
        {
            this._driver = driver;
        }
        IWebElement _usernameField => _driver.FindElement(By.Id("username"));
        IWebElement _passwordField => _driver.FindElement(By.Id("password"));
        IWebElement _loginButton => _driver.FindElement(By.Name("login"));


        //Finds the login fields and sends values based on how they are stored.
        public void SetUsername(string username)
        {
            _usernameField.Clear();
            _usernameField.SendKeys(username);
            
        }

        public void SetPassword(string password)
        {
            _passwordField.Clear();
            _passwordField.SendKeys(password);
        }

        public void ClickLogin()
        {
            _loginButton.Click();
        }

    }
}
