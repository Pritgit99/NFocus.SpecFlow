using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.POMClasses
{
    internal class HomePagePOM
    {
        private IWebDriver _driver; //field to hold webdriver instance
    
        public HomePagePOM(IWebDriver driver) //get the webdriver instance from the calling test
        {
           this._driver = driver;  
        }

        //Locators
       IWebElement myaccountLink => _driver.FindElement(By.LinkText("My account"));


        //service method
        public void MyAccountPage()
        {
            myaccountLink.Click(); //Directs users to the account page 

        }
             
    }
}
