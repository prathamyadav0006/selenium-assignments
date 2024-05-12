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
    public class Tests
    {
        public Actions actions = new Actions();
        [SetUp]
        public void Setup()
        {
            actions.GoToSite();
        }
        [Test]
        public void Test1()
        {
            // object to fetch data from the excel sheet
            var personalDetails = new PersonalDetails();

            string boardingCityName = personalDetails.GetBoardingCityName();
            string destinationCityName = personalDetails.GetDestinationCityName();

            // To select the boarding and destination
            actions.SelectFromAndTo(boardingCityName, destinationCityName);

            Dictionary<string, int> travellersCount = personalDetails.TravellersCount();
            string travelingClassName = personalDetails.GetTravellingClassName();

            // To select travellers count and for choosing travelling class
            // (adult count, children count,infant count, travelling class (E(Economy)/PE(Premium Economy)/B(Buisness)/F(First)))
            actions.SelectNumberOfTravellersAndClass(travellersCount["Adult"], travellersCount["Children"], travellersCount["Infant"], travelingClassName);

            // To choose the flight
            string roundTrip = personalDetails.GetRoundTripOption();

            if (roundTrip == "yes")
            {
                actions.SelectRoundTripFlight();
            }
            else
            {
                actions.SelectFlight();
            }
            // To fill traveller contact details
            Dictionary<string, string> travellerContactDetails = personalDetails.TravellerContactDetails();
            actions.FillTravellerContactDetails(travellerContactDetails["insuranceChoice"], travellerContactDetails["email"], travellerContactDetails["phonenumber"]);

            // Holds all the travellers perdetails
            List<List<string>> travellersDetails = personalDetails.GetTravellersDetails();

            // To fill travellers names
            actions.FillTravellerPersonalDetails(travellersDetails);

            // To complete the details
            //if(!roundTrip)

            actions.CompleteBooking();
            Assert.Pass("Flight booking completed");
        }
    }
}