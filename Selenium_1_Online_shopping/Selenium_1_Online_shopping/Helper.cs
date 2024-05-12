/*
    Pratham Yadav
    Assignment 3
    Problem Statement:
        1) Go to amazon.in, search for at least 5 different products, add to cart and check out
        2) Close the browser and reopen and do the same 1st step, 5 times
        3) Double check the cart total every time and print the cart items at 2nd step 
*/

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium_1_Online_shopping;

namespace Helper
{
    public class Actions
    {
        SeleniumCustomMethods customMethod = new SeleniumCustomMethods();

        // Utility private variables
        private readonly IWebDriver driver = new ChromeDriver();
        private string password;
        private string phoneNumber;

        // Utility methods

        // To sleep the process
        public void Wait(int timeInSecond)
        {
            Thread.Sleep(timeInSecond * 1000);
            return;
        }
        // To fill credentials in order to login
        public void InitializeCredentials()
        {
            Wait(2000);

            string phoneNumber = "", password = "";
            using (var reader = new StreamReader(@"C:\Users\prathamy\OneDrive - NVIDIA Corporation\amazon_credentials.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    phoneNumber = values[0];
                    password = values[1];
                }

            }
            this.phoneNumber = phoneNumber;
            this.password = password;
        }

        // To search items on website

        public void SearchItem(string itemName)
        {
            customMethod.SendKeyByIdAndSubmit(driver, "twotabsearchtextbox", itemName);

            // Get First Element from the itemList
            AddToCart(itemName);
        }

        // To add item in the cart
        public void AddToCart(string itemName)
        {
            Console.WriteLine($"Adding {itemName} to the cart");

            //driver.FindElement(By.XPath("//*[@id=\"a-autoid-1-announce\"]")).Click();
            Wait(5);

            // Code block to handle no-result found
            try
            {
                Console.WriteLine("Selecting item");
                IWebElement itemImage = driver.FindElement(By.ClassName("s-image"));
                customMethod.ClickElementByClassName(driver, "s-image");
                Wait(6);

                Console.WriteLine("Switching to new tab");
                // switching between previous tab and active tab
                driver.SwitchTo().Window(driver.WindowHandles[1]);

                // implementing the implicit wait for loading the searched item
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                Console.WriteLine($"Adding {itemName} to the cart");
                // selecting the add to cart button
                customMethod.ClickElementById(driver, "add-to-cart-button");

                Wait(4);

                Console.WriteLine("Closing new tab");
                driver.Close();

                Console.WriteLine("Navigating to old tab");
                // switching back to the intial window
                driver.SwitchTo().Window(driver.WindowHandles[0]);
            }
            catch (Exception ex)
            {
                driver.Navigate().Back();
                Console.WriteLine("Item {0} not found", itemName);
                return;
            }
        }

        // Functional methods

        // To Open Amazon.in
        public void OpenAmazon()
        {
            Console.WriteLine("Opening Amazon.in");

            driver.Navigate().GoToUrl("https://www.amazon.in/");

            driver.Manage().Window.Maximize();

            // handling captcha
            try
            {
                Console.WriteLine("Captcha opened handle manually");
                IWebElement captchaWindow = driver.FindElement(By.Id("captchacharacters")) == null ? null : driver.FindElement(By.Id("captchacharacters"));
                Wait(15);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // To login on Amazon
        public void LoginToAmazon()
        {
            Console.WriteLine("Login into Amazon using credentials");
            InitializeCredentials();

            customMethod.ClickElementById(driver, "nav-link-accountList-nav-line-1");

            customMethod.SendKeyByIdAndSubmit(driver, "ap_email", phoneNumber);

            customMethod.SendKeyByNameAndSubmit(driver, "password", password);
        }

        // To Start shopping
        public void StartShopping(List<string> itemsDetails, ref List<string> checkOutAmountForAllIterations, int iterationCount)
        {
            Console.WriteLine($"{iterationCount}th Iteration started");
            for (int i = 0; i < itemsDetails.Count; i++)
            {
                Console.WriteLine($"Searching {i + 1} item");
                SearchItem(itemsDetails[i]);
            }

            Wait(4);

            // Checkin out cart
            Console.WriteLine($"Extracting final checkout amount of {iterationCount}th");

            customMethod.ClickElementByXpath(driver, @"//*[@id=""ewc-compact-actions-container""]/div/div[2]/span/span");

            Wait(4);

            string checkOutAmountDiv = @"//*[@id=""sc-subtotal-amount-buybox""]/span";
            string checkOutAmountValue = customMethod.GetTextByXpath(driver, checkOutAmountDiv);

            checkOutAmountForAllIterations.Add(checkOutAmountValue);

            // closing the driver.
            driver.Quit();
        }
    }
}
