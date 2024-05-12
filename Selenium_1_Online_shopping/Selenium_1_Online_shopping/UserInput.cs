/*
    Pratham Yadav
    Assignment 3
    Problem Statement:
        1) Go to amazon.in, search for at least 5 different products, add to cart and check out
        2) Close the browser and reopen and do the same 1st step, 5 times
        3) Double check the cart total every time and print the cart items at 2nd step 
*/

using IronXL;
namespace Selenium_1_Online_shopping
{
    internal class UserInput
    {
        public static string filePath = @"C:\Users\prathamy\Pratham\Assignments\SeleniumAssignments\Selenium_1_Online_shopping\Selenium_1_Online_shopping\Shopping_List.xlsx";
        public WorkSheet workSheet = WorkBook.Load(filePath).WorkSheets.First();
        public List<string> colNameList = new List<string>(["A", "B", "C", "D", "E"]);
       
        // To get the list of items for each iteration
        public List<List<string>> GetItemsLists()
        {
            var itemList = new List<List<string>>();

            Console.WriteLine("Extracting items list from the Excel\n");
            for (int i = 0; i < 1; i++)
            {
                var tempList = new List<string>();
                for (int j = 0; j < 3; j++)
                {
                    int rowNum = j + 1;
                    string cellName = colNameList[i] + rowNum.ToString();
                    tempList.Add(workSheet[cellName].ToString());
                }
                itemList.Add(tempList);
            }

            return itemList;
        }

        // To set the checkout amount for each iteration and list of items.
        public void StoreCheckOutAmount(List<string> checkOutAmountForAllIterations)
        {
            Console.WriteLine("Writing checkout amount per item list\n");
            for (int i = 0; i < checkOutAmountForAllIterations.Count; i++)
            {
                workSheet[$"{colNameList[i]}6"].Value = checkOutAmountForAllIterations[i];
            }
            workSheet.SaveAs(filePath);
        }
    }
}
