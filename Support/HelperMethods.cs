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


public class HelperMethods
{
    private IWebDriver _driver;

    public HelperMethods(IWebDriver driver)
    {
        this._driver = driver;
    }
    
    //Multiple waits that are used in different context
    public static void WaitForElDisplayed(By locator, int timeToWaitInSeconds, IWebDriver driver)
    {
        WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWaitInSeconds));
        wait2.Until(drv => drv.FindElement(locator).Displayed);
    }

    public static IWebElement WaitForElement(By locator, int timeToWaitInSeconds, IWebDriver driver) // used to apply waits around the program
    {
        WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWaitInSeconds));
        return waitForElement.Until(drv => drv.FindElement(locator));
    }

    public static void WaitFor(Func<bool> condition, int timeoutInSeconds, IWebDriver driver)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        wait.Until(d => condition());
    }

    //Used to take screenshots of bugs that occur
    public static void TakeScreenshotOfElement(IWebDriver driver, IWebElement element, string filename) //take a screenshot of bug/errors
    {
        ITakesScreenshot ssBug = driver as ITakesScreenshot;
        Screenshot screenshot = ssBug.GetScreenshot();
        var screenshotForm = ssBug.GetScreenshot();
        string timeCaptured = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        screenshotForm.SaveAsFile(@"C:\Users\PriteshPanchal\Documents\BugScreenshots\" + timeCaptured + " " + filename, ScreenshotImageFormat.Png); //ToDo: Timstamp screenshots so they are not overwritten
        
        TestContext.WriteLine("Screenshot taken - see report");
        TestContext.AddTestAttachment(@"C:\Users\PriteshPanchal\Documents\BugScreenshots\" + timeCaptured +" " + filename);

    }

    //Tests for null values when fetching from runsettings file
    public static string GetParameterValue(string parameterName)
    {
        string parameterValue = TestContext.Parameters[parameterName];

        if (string.IsNullOrEmpty(parameterValue))
        {
            throw new ArgumentException($"The parameter '{parameterName}' is null or empty in the test settings.");
        }

        return parameterValue;
    }

}
