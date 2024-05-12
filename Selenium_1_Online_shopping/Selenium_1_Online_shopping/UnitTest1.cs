/*
    Pratham Yadav
    Assignment 3
    Problem Statement:
        1) Go to amazon.in, search for at least 5 different products, add to cart and check out
        2) Close the browser and reopen and do the same 1st step, 5 times
        3) Double check the cart total every time and print the cart items at 2nd step 
*/

using Helper;

namespace Selenium_1_Online_shopping
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            var userInput = new UserInput();
            var collectionOfItemList = userInput.GetItemsLists();
            //List<List<string>> collectionOfItemList = [["Ball", "Key"], ["Cup", "Card"], ["Exam pad", "Pen"]];

            Thread.Sleep(3000);

            // Store list of items for each iteration
            var checkOutAmountForAllIterations = new List<string>();

            // Iterating over the list of items to do shopping
            for (int i = 0; i < collectionOfItemList.Count; i++)
            {
                var itemList = collectionOfItemList[i];
                var actions = new Actions();

                actions.OpenAmazon();
                //actions.LoginToAmazon
                actions.StartShopping(itemList, ref checkOutAmountForAllIterations, i+1);
            }

            // Storing checkout amount for each item list.
            userInput.StoreCheckOutAmount(checkOutAmountForAllIterations);
        }
    }
}