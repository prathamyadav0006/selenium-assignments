/*
    Pratham Yadav
    Assignment 3
    Problem Statement:
        1) Go to amazon.in, search for at least 5 different products, add to cart and check out
        2) Close the browser and reopen and do the same 1st step, 5 times
        3) Double check the cart total every time and print the cart items at 2nd step 
*/

using OpenQA.Selenium;
namespace Selenium_1_Online_shopping
{
    internal class SeleniumCustomMethods
    {
        // To click the web element by using Xpath
        public void ClickElementByXpath(IWebDriver driver, string elementXpath)
        {
            try
            {
                Console.WriteLine("Clicking element with Xpath: {0}", elementXpath);
                IWebElement webElement = driver.FindElement(By.XPath(elementXpath));
                webElement.Click();
            }
            catch (Exception ex) { driver.Quit(); Assert.Fail("Web element with Xpath {0} not found \n Error: {1}", elementXpath, ex.Message); }
        }

        // To click the web element by using class name
        public void ClickElementByClassName(IWebDriver driver, string elementClassName)
        {
            try
            {
                Console.WriteLine("Clicking element with class name: {0}", elementClassName);
                IWebElement webElement = driver.FindElement(By.ClassName(elementClassName));
                webElement.Click();
            }
            catch (Exception ex) { driver.Quit(); Assert.Fail("Web element with Class name {0} not found \n Error: {1}", elementClassName, ex.Message); }
        }

        // To click the web element by using Id
        public void ClickElementById(IWebDriver driver, string elementId)
        {
            try
            {
                Console.WriteLine("Clicking element with Id: {0}", elementId);
                IWebElement webElement = driver.FindElement(By.Id(elementId));
                webElement.Click();

            }
            catch (Exception ex) { driver.Quit(); Assert.Fail("Web element with ID {0} not found \n Error: {1}", elementId, ex.Message); }

        }
        // To send text to the input by using Xpath
        public void SendKeyByXpath(IWebDriver driver, string elementXpath, string textString)
        {
            try
            {
                Console.WriteLine("Sending input text string to the element with Xpath: {0}", elementXpath);
                IWebElement webElement = driver.FindElement(By.XPath(elementXpath));
                webElement.Clear();
                webElement.SendKeys(textString);
            }
            catch (Exception ex) { driver.Quit(); Assert.Fail("Web element with Xpath {0} not found \n Error: {1}", elementXpath, ex.Message); }
        }

        // To send text to the web element using id and submitting the form
        public void SendKeyByIdAndSubmit(IWebDriver driver, string elementId, string textString)
        {
            try
            {
                Console.WriteLine("Sending input text string to the element with Id: {0}", elementId);
                IWebElement webElement = driver.FindElement(By.Id(elementId));
                webElement.Clear();
                webElement.SendKeys(textString);
                webElement.Submit();
            }
            catch (Exception ex) { driver.Quit(); Assert.Fail("Web element with ID {0} not found \n Error: {1}", elementId, ex.Message); }
        }

        // To send text to the web element using class name and submitting the form 
        public void SendKeyByNameAndSubmit(IWebDriver driver, string elementName, string textString)
        {
            try
            {
                Console.WriteLine("Sending input text string to the element with name: {0}", elementName);
                IWebElement webElement = driver.FindElement(By.ClassName(elementName));
                webElement.Clear();
                webElement.SendKeys(textString);
                webElement.Submit();
            }
            catch (Exception ex) { driver.Quit(); Assert.Fail("Web element with name {0} not found \n Error: {1}", elementName, ex.Message); }
        }

        // To get the text of the web element
        public string GetTextByXpath(IWebDriver driver, string elementXpath)
        {
            try
            {
                Console.WriteLine("Getting text from the element with Xpath: {0}", elementXpath);
                IWebElement webElement = driver.FindElement(By.XPath(elementXpath));
                return webElement.Text;
            }
            catch (Exception ex) { driver.Quit(); Assert.Fail("Web element with Xpath {0} not found \n Error: {1}", elementXpath, ex.Message); }

            return "Error while scrapping text";
        }
    }
}
