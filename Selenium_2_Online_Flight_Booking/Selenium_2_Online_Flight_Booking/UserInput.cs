/*
    Pratham Yadav
    Assignment 4
    Problem Statement:
        1) Pick any travel website you choose, search for a flight and automate it till payment gateway page
        2) Try one-way, two-way, multi-travels, different class, etc combinations
        3) Extract the lowest & highest cost for that day
*/

using IronXL;

namespace Selenium_2_Online_Flight_Booking
{
    public class PersonalDetails
    {
        public static string filePath = @"C:\Users\prathamy\Pratham\Assignments\SeleniumAssignments\Selenium_2_Online_Flight_Booking\Selenium_2_Online_Flight_Booking\passenger details.xlsx";
        public WorkSheet workSheet = GetWorkBook(filePath);
        public static int totalTravellersCount = 0;

        // To initialize worksheet
        public static WorkSheet GetWorkBook(string filePath)
        {
            try
            {
                WorkBook workBook = WorkBook.Load(filePath);
                WorkSheet workSheet = workBook.WorkSheets[0];
                return workSheet;
            }
            catch (Exception ex)
            {
                Assert.Fail("Unable to access excel sheet\n" + ex.Message);
            }
            return null;
        }

        // To get Boarding city name
        public string GetBoardingCityName()
        {
            string boardingCity = workSheet["B1"].ToString();
            Console.WriteLine("Selecting boarding city: {0}", boardingCity);
            return boardingCity;
        }

        // To get Desitnation city name
        public string GetDestinationCityName()
        {
            string destinationCity = workSheet["B2"].ToString();
            Console.WriteLine("Selecting destination city: {0}", destinationCity);
            return destinationCity;
        }

        // To get round trip option
        public string GetRoundTripOption()
        {
            string roundTripOption =  workSheet["B3"].ToString();
            Console.WriteLine("Round trip status: {0}", roundTripOption);
            return roundTripOption;
        }

        // To get Travelling
        public string GetTravellingClassName()
        {
            string travellingClass =  workSheet["B15"].ToString();
            Console.WriteLine("Travelling class: {0}", travellingClass);
            return travellingClass;
        }

        // To get the total number of travellers with respect to the category.
        public Dictionary<string, int> TravellersCount()
        {
            Console.WriteLine("Extracting travellers count w.r.t category from the excel sheet");
            var travellersCountWithCategory = new Dictionary<string, int>();

            for (int i = 5; i <= 7; i++)
            {
                int count = workSheet[$"B{i}"].Int32Value;
                travellersCountWithCategory[workSheet[$"A{i}"].ToString()] = count;
                totalTravellersCount += count;
            }

            return travellersCountWithCategory;
        }

        // To extract traverller contact details and choice of insurance opt from the excel
        public Dictionary<string, string> TravellerContactDetails()
        {
            Console.WriteLine("Extracting contact details of traveller");
            var travellerContactDetails = new Dictionary<string, string>();

            travellerContactDetails["insuranceChoice"] = workSheet["B18"].ToString();
            travellerContactDetails["email"] = workSheet["B19"].ToString();
            travellerContactDetails["phonenumber"] = workSheet["B20"].ToString();

            return travellerContactDetails;
        }


        // To get the traveller's information from the excel containing (First Name, Last Name, Title, Category)
        public List<List<string>> GetTravellersDetails()
        {
            Console.WriteLine("Extracting personal details of all the travellers");
            var travellersDetails = new List<List<string>>();
            int rowNum = 24;

            while (totalTravellersCount > 0)
            {
                var temp = new List<string>();

                string title = workSheet[$"A{rowNum}"].ToString();
                string firstName = workSheet[$"B{rowNum}"].ToString();
                string lastName = workSheet[$"C{rowNum}"].ToString();
                string category = workSheet[$"D{rowNum}"].ToString();


                temp.Add(title);
                temp.Add(firstName);
                temp.Add(lastName);
                temp.Add(category);

                travellersDetails.Add(temp);
                rowNum++;
                totalTravellersCount--;
            }
            return travellersDetails;
        }
        // To set the lowest and highest prices for the selected boarding-destination cities.
        public void SetLowestHightestPriceInExcel(int minPrice, int maxPrice, string minPriceCell, string maxPriceCell)
        {
            Console.WriteLine("Putting maximum price {0} and minimum price {1} in excel", maxPrice, minPrice);
            workSheet[maxPriceCell].Value = maxPrice.ToString();
            workSheet[minPriceCell].Value = minPrice.ToString();
            workSheet.SaveAs(filePath);
        }
    }
}
