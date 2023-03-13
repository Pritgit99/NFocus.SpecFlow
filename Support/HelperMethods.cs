using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using NUnit.Framework;




namespace SpecFlowProject.Support;


internal class HelperMethods
{
    private IWebDriver _driver;

    public HelperMethods(IWebDriver driver)
    {
        this._driver = driver;
    }


    public static IWebElement WaitForElement(By locator, int timeToWaitInSeconds, IWebDriver driver) // used to apply waits around the program
    {
        WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWaitInSeconds));
        return waitForElement.Until(drv => drv.FindElement(locator));

    }

    public static void TakeScreenshotOfElement(IWebDriver driver, By locator, string filename) //take a screenshot of bug/errors
    {
        IWebElement ssdriver = driver.FindElement(locator);
        ITakesScreenshot ssBug = ssdriver as ITakesScreenshot;
        var screenshotForm = ssBug.GetScreenshot();
        string timeCaptured = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        screenshotForm.SaveAsFile(@"C:\Users\PriteshPanchal\Documents\BugScreenshots\" + timeCaptured + " " + filename, ScreenshotImageFormat.Png); //ToDo: Timstamp screenshots so they are not overwritten
        
        TestContext.WriteLine("Screenshot taken - see report");
        TestContext.AddTestAttachment(@"C:\Users\PriteshPanchal\Documents\BugScreenshots\" + timeCaptured +" " + filename);

    }

}
