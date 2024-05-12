/*
    Pratham Yadav
    Assignment 4
    Problem Statement:
        1) Pick any travel website you choose, search for a flight and automate it till payment gateway page
        2) Try one-way, two-way, multi-travels, different class, etc combinations
        3) Extract the lowest & highest cost for that day
*/
using OpenQA.Selenium;

namespace Selenium_2_Online_Flight_Booking
{
    internal class SeleniumCustomMethods
    {
        // To click the web element using the Xpath
        public void ClickElementByXpath(IWebDriver driver, string elementXpath)
        {
            try
            {
                Console.WriteLine($"Clicking element with Xpath: {elementXpath}");
                IWebElement webElement = driver.FindElement(By.XPath(elementXpath));
                webElement.Click();
            }
            catch (Exception ex) { driver.Quit(); Assert.Fail("Web element with Xpath {0} not found \n Error: {1}", elementXpath, ex.Message);  }
        }

        // To click the web element by using the class name
        public void ClickElementByClassName(IWebDriver driver, string elementClassName)
        {
            try
            {
                Console.WriteLine($"Clicking element with class name: {elementClassName}");
                IWebElement webElement = driver.FindElement(By.ClassName(elementClassName));
                webElement.Click();
            }
            catch (Exception ex) { driver.Quit(); Assert.Fail("Web element with Class name {0} not found \n Error: {1}", elementClassName, ex.Message); }
        }

        // To send the text to the selected web element
        public void SendKeyByXpath(IWebDriver driver, string elementXpath, string textString)
        {
            try
            {
                Console.WriteLine($"Sending text to input element with Xpath: {elementXpath}");
                IWebElement webElement = driver.FindElement(By.XPath(elementXpath));
                webElement.SendKeys(textString);
            }
            catch (Exception ex) { driver.Quit(); Assert.Fail("Web element with Xpath {0} not found \n Error: {1}", elementXpath, ex.Message); }
        }
    }
}
