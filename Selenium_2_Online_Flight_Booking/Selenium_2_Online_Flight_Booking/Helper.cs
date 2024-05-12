/*
    Pratham Yadav
    Assignment 4
    Problem Statement:
        1) Pick any travel website you choose, search for a flight and automate it till payment gateway page
        2) Try one-way, two-way, multi-travels, different class, etc combinations
        3) Extract the lowest & highest cost for that day
*/

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;



namespace Selenium_2_Online_Flight_Booking
{
    
    public class Actions
    {
        private IWebDriver driver = new ChromeDriver();
        private SeleniumCustomMethods customMethod = new SeleniumCustomMethods();
        PersonalDetails personalDetails = new PersonalDetails();


        // Utility methods

        // To add wait in the process
        public void Wait(int duration)
        {
            duration = duration * 1000;
            Thread.Sleep(duration);
        }

        // to opne easemy trip website
        public void GoToSite()
        {
            Console.WriteLine("Visiting easemytrip");
            driver.Navigate().GoToUrl("https://www.easemytrip.com");
            driver.Manage().Window.Maximize();
            Wait(6);
        }

        // To select the boarding city and destination city
        public void SelectFromAndTo(string fromCity, string toCity)
        {
            Console.WriteLine("Selecting boarding and destination cities");
            Wait(2);

            string fromCityBanner = "//*[@id=\"frmcity\"]";
            customMethod.ClickElementByXpath(driver, fromCityBanner);

            Wait(2);

            string fromCityInput = ("//*[@id=\"a_FromSector_show\"]");
            customMethod.SendKeyByXpath(driver, fromCityInput, fromCity);

            Wait(2);

            string fromCitySelection = "//*[@id=\"fromautoFill\"]/ul/li";
            customMethod.ClickElementByXpath(driver, fromCitySelection);

            Wait(2);

            string toCityInput = "//*[@id=\"a_Editbox13_show\"]";
            customMethod.SendKeyByXpath(driver, toCityInput, toCity);

            Wait(4);

            string toCitySelection = "//*[@id=\"toautoFill\"]/ul/li";
            customMethod.ClickElementByXpath(driver, toCitySelection);
            
            Wait(2);

            // selecting the active date and click on current travellers 
            string currentDate = "active-date";
            customMethod.ClickElementByClassName(driver, currentDate);
        }

        // To select the counts of travellers
        public void SelectNumberOfTravellersAndClass(int adultCount, int childrenCount, int infantCount, string travelingClassName)
        {
            Console.WriteLine("Selecting number of travellers");
            Wait(2);

            string travellersInfoBanner = "//*[@id=\"myFunction4\"]";
            customMethod.ClickElementByXpath(driver, travellersInfoBanner);

            if (adultCount <= 0 || childrenCount < 0 || infantCount < 0)
            {
                Assert.Fail("Adult/Children/Infant count cannot be 0 and negative");
            }

            Wait(3);

            // To select the adult, child and infant count
            string adultCountBtn = "//*[@id=\"add\"]";

            adultCount--;

            while (adultCount != 0)
            {
                customMethod.ClickElementByXpath(driver, adultCountBtn);
                adultCount--;
            }

            string childrenCountBtn = "/html/body/form/div[5]/div[3]/div/div[3]/div/div[6]/div[2]/div/div[2]/div[2]/button[2]";
            while (childrenCount != 0)
            {
                customMethod.ClickElementByXpath(driver, childrenCountBtn);
                childrenCount--;
            }

            string infantCountBtn = "/html/body/form/div[5]/div[3]/div/div[3]/div/div[6]/div[2]/div/div[3]/div[2]/button[2]"; ;
            while (infantCount != 0)
            {
                customMethod.ClickElementByXpath(driver, infantCountBtn);
                infantCount--;
            }

            // to select the class for traveling
            try
            {
                if (travelingClassName == "E")
                {
                    customMethod.ClickElementByXpath(driver, "//*[@id=\"lbEconomy\"]");
                }
                else if (travelingClassName == "PE")
                {
                    customMethod.ClickElementByXpath(driver, "//*[@id=\"lbpEconomy\"]");
                }
                else if (travelingClassName == "B")
                {
                    customMethod.ClickElementByXpath(driver, "//*[@id=\"lbBusiness\"]");
                }
                else if (travelingClassName == "F")
                {
                    customMethod.ClickElementByXpath(driver, "//*[@id=\"lbFirst\"]");
                }
                else
                {
                    Assert.Fail("Invalid traveling class selected");
                }

                Wait(2);
                customMethod.ClickElementByXpath(driver, "//*[@id=\"traveLer\"]");

                Wait(4);
                customMethod.ClickElementByXpath(driver, "//*[@id=\"showOWRT\"]/div/div[8]/button");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        // To fetch lowest and highest prices for roundtrips IN
        public void FetchLowestAndHighestFlightRatesForRoundTrip(string flightDirection, string divNum, string lowestPriceCell, string highestPriceCell)
        {
            Console.WriteLine("Extracting highest and lowest price for round trip from the web page");
            Wait(5);
            //*[@id="DivOut0"]/div[6]/div/div[2]
            int maxPrice = -1,
                minPrice = int.MaxValue;

            int i = 0;
            do
            {
                try
                {//*[@id="DivOut5"]/div[6]/div/div[2]
                    //*[@id="DivIN4"]/div[6]/div/div[3]
                    //*[@id="DivIN21"]/div[6]/div[1]/div[3]

                    IWebElement priceBox = driver.FindElement(By.XPath($@"//*[@id=""Div{flightDirection}{i}""]/div[6]/div/div[{divNum}]"));
                    int price = int.Parse(priceBox.Text, NumberStyles.AllowThousands, new CultureInfo("en-au")); ;

                    maxPrice = price > maxPrice ? price : maxPrice;
                    minPrice = price < minPrice ? price : minPrice;

                    i++;
                }
                catch (Exception ex)
                {
                    personalDetails.SetLowestHightestPriceInExcel(minPrice, maxPrice, lowestPriceCell, highestPriceCell);
                    Console.WriteLine($"priceBox with id spnPrice{i} not present" + ex.Message);
                    return;
                }

            } while (true);
        }
        
        // To select round trip flight
        public void SelectRoundTripFlight()
        {
            Console.WriteLine("Selecting the round trip fligt");
            Wait(15);
            string roundTripBtn = @"//*[@id=""ServiceControllerId""]/div[4]/div/div[4]/div/div/div[1]/div/div[2]/div[1]/div[1]/ul/li[2]";
            customMethod.ClickElementByXpath(driver, roundTripBtn);

            // to click the calender
            customMethod.ClickElementByXpath(driver, @"//*[@id=""rdate""]");

            Wait(3);
            // to select return date
            customMethod.ClickElementByXpath(driver, @"//*[@id=""30/03/2024""]");

            // to search the flights
            customMethod.ClickElementByXpath(driver, @"//*[@id=""btnSrch""]");

            Wait(15);
            
            FetchLowestAndHighestFlightRatesForRoundTrip("Out", "2", "D7", "E7");

            Wait(5);
            
            FetchLowestAndHighestFlightRatesForRoundTrip("IN", "3", "F7", "G7");

            // to book both flights
            customMethod.ClickElementByXpath(driver, @"//*[@id=""BtnBookNow""]");

            Wait(10);

            // continue btn
            customMethod.ClickElementByXpath(driver, @"//*[@id=""DivMoreFareRT""]/div/div[3]/div/div/div[2]");

            Wait(4);
        }

        // To fetch lowest and highest flight rates for selected class
        public void FetchLowestAndHighestFlightRates()
        {
            Console.WriteLine("Extracting highest and lowest flight prices from the web page");
            Wait(4);//*[@id="spnPrice1"]

            int maxPrice = -1,
                minPrice  = int.MaxValue;

            int i = 1;
            do
            {
                try
                {
                    IWebElement priceBox = driver.FindElement(By.XPath($@"//*[@id=""spnPrice{i}""]"));
                    int price = int.Parse(priceBox.Text, NumberStyles.AllowThousands, new CultureInfo("en-au")); ;

                    maxPrice = price > maxPrice ? price : maxPrice;
                    minPrice = price < minPrice ? price : minPrice;

                    i++;
                }
                catch(Exception ex)
                {
                    personalDetails.SetLowestHightestPriceInExcel(minPrice, maxPrice, "D3", "E3");
                    Console.WriteLine($"priceBox with id spnPrice{i} not present" + ex.Message);
                    return;
                }
                
            } while (true);

        }
        // To select flight 
        public void SelectFlight()
        {
            // Xpath of this button keeps on changing
            //*[@id="ResultDiv"]/div/div/div[4]/div[2]/div[1]/div[2]/div[6]/button
            //*[@id="ResultDiv"]/div/div/div[4]/div[2]/div[1]/div[1]/div[6]/button[1]
            //*[@id="ResultDiv"]/div/div/div[4]/div[2]/div[1]/div[2]/div[6]/button
            //*[@id="ResultDiv"]/div/div/div[4]/div[2]/div[1]/div[1]/div[6]/button[1]
            //*[@id="ResultDiv"]/div/div/div[4]/div[2]/div[1]/div[1]/div[6]/button[1]
            Console.WriteLine("Selecting the flight");
            Wait(5);
            FetchLowestAndHighestFlightRates();

            string selectFlightBtn = "//*[@id=\"ResultDiv\"]/div/div/div[4]/div[2]/div[1]/div[1]/div[6]/button[1]";
            customMethod.ClickElementByXpath(driver, selectFlightBtn);
        }

        // To fill traveller contact details
        public void FillTravellerContactDetails(string tripInsurance, string passengerEmail, string passengerPhoneNumber)
        {
            Console.WriteLine("Filling traveller contact information");
            // To select trip insurance
            string insurBtn;
            Wait(2);

            if (tripInsurance == "yes")
            {
                insurBtn = "//*[@id=\"divInsuranceTab\"]/div[3]/div[1]/label";
            }
            else
            {
                insurBtn = "//*[@id=\"divInsuranceTab\"]/div[3]/div[3]/label";
            }

            customMethod.ClickElementByXpath(driver, insurBtn);

            // To set email and phone number

            string emailInput = "//*[@id=\"txtEmailId\"]";
            customMethod.SendKeyByXpath(driver, emailInput, passengerEmail);

            string submitEmailBtn = "//*[@id=\"spnVerifyEmail\"]";
            customMethod.ClickElementByXpath(driver, submitEmailBtn);

            Wait(4);

            string phoneNumberInput = "//*[@id=\"txtCPhone\"]";
            customMethod.SendKeyByXpath(driver, phoneNumberInput, passengerPhoneNumber);

        }

        // To fill the personal details of the travellers
        public void FillTravellerPersonalDetails(List<List<string>> travellersDetails)
        {
            Console.WriteLine("Filling traveller personal information");
            string prevCategory = "";
            int count = 0;
            for (int i = 0; i < travellersDetails.Count; i++)
            {
                List<string> travellerDetail = travellersDetails[i];
                if (prevCategory != travellerDetail[3])
                {
                    prevCategory = travellerDetail[3];
                    count = 0;
                }
                SelectTravellerPersonalDetails(travellerDetail[0], travellerDetail[1], travellerDetail[2], travellerDetail[3], count.ToString());
                count++;
            }
        }
        // To fill the personal details of a traveller for flight booking
        public void SelectTravellerPersonalDetails(string title, string firstName, string lastName, string category, string count)
        {
            Wait(4);
            string option = "0";

            if (title == "Mr" || title == "Miss")
                option = "2";
            else if (title == "Ms" || title == "Mstr")
                option = "3";
            else if (title == "Mrs")
                option = "4";


            string titleBtn = $@"//*[@id=""title{category}{count}""]";
            customMethod.ClickElementByXpath(driver, titleBtn);

            Wait(3);

            string titleName = $@"//*[@id=""title{category}{count}""]/option[{option}]";
            customMethod.ClickElementByXpath(driver, titleName);


            Wait(4);

            string firstNameInput = $@"//*[@id=""txtFN{category}{count}""]";
            customMethod.SendKeyByXpath(driver, firstNameInput, firstName);

            string lastNameInput = $@"//*[@id=""txtLN{category}{count}""]";
            customMethod.SendKeyByXpath(driver, lastNameInput, lastName);

            Wait(2);


            if (category == "Infant")
            {
                SelectDOB(count, "Day");
                SelectDOB(count, "Mon");
                SelectDOB(count, "Yar");
            }

        }
        // to select Date of Birth for the infant category
        public void SelectDOB(string count, string dobOption)
        {
            customMethod.ClickElementByXpath(driver, $@"//*[@id=""divDOB{dobOption}Infant{count}""]");
            customMethod.ClickElementByXpath(driver, $@"//*[@id=""divDOB{dobOption}Infant{count}""]/option[2]");
        }

        // To click the continue booking buttons which come at last
        public void CompleteBooking()
        {
            driver.Quit();

            /*Wait(4);

            customMethod.ClickElementByXpath(driver, @"//*[@id=""spnTransaction""]");

            Wait(2);

            customMethod.ClickElementByXpath(driver, @"//*[@id=""DivContinueAncillary""]/span");

            Wait(2);

            customMethod.ClickElementByXpath(driver, @"//*[@id=""DivContinueAncillary""]/span");
            */
        }
    }
}

