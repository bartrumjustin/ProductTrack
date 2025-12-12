
using System;
using System.Diagnostics;
using System.Xml.Linq;

using System.Data.Common;
using System.Transactions;
using System.Runtime.InteropServices;

namespace FinalProject
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            
            char charKey = 'a';

            Console.WriteLine("Welcome to my Final Project!\n" +
                "This console application is meant to act as a simplified version of a Product Inventory/tracking system\n" +
               
                "How To Select: As you navigate will find letters wrapped in []\n" +
                "these are the trigger letters used to initiate an action\n"+
                "[press any key] to continue");
            Console.ReadKey();
            
            while (charKey != 'Q')
            {
                Console.WriteLine("Welcome to the Inventory System\n" +
                              "-----------------------------------------\n\n" +
                              "Please Enter Your Selection...  \n\n" +
                              "[C]reate Product              \n\n" +
                              "[V]iew List             \n\n" +
                              
                              "[Q] to quit program                      \n\n" +
                              "-----------------------------------------\n\n");
                
                string stringKey = Console.ReadLine().ToUpper();
                while (!char.TryParse(stringKey, out charKey) || string.IsNullOrWhiteSpace(stringKey) == true)
                {
                    Console.WriteLine("Please provide a single selction from the provided options.");
                    stringKey = Console.ReadLine().ToUpper();
                }
                switch (charKey)
                {
                    case 'C':
                        //port to Create Function
                        Console.Clear();
                        Create();
                        break;
                    case 'V':
                        //Port to Modify Function
                        Console.Clear();
                        View();
                        break;
                    case 'Q':
                        Console.Clear();
                        Console.WriteLine("GoodBye");
                        break;
                    
                }
            }

            
            
        }
        public static void Create()
        {
            int prodId = 0;
            string name = string.Empty;
            
            string type = string.Empty;
            int quantity = 0;
            double price = 0;

            string signature = string.Empty;
            bool quit = false;
            
            Console.WriteLine("You have entered into Creation Mode...\n\n" +
                              "**************************************\n" +
                              "Enter your first name\n" +
                              "[Q] to quit");
            signature = Console.ReadLine();
            char test = ' ';
            try
            {
                test = signature[0];
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine("Nothing was entered, Exiting Creation Mode\n" +
                    $"details: {ex.Message}\n\n" +
                    $"[any key to continue]");

                test = 'q';
                Console.ReadKey();
                Console.Clear();
            }
            if (char.ToUpper(test).Equals('Q') == true) {
                quit = true;
                Console.Clear();
            }
            //validate signature is a name :Done
            while (quit == false) {
                Console.Clear();
                Object newObj= new Object();
                newObj.Signed = signature; 
                Console.WriteLine("Enter Product ID");
                while(!int.TryParse(Console.ReadLine(), out prodId)){
                    Console.WriteLine("Error: Enter a valid numeric Id");
                }
                newObj.ProdId = prodId;
                Console.Clear();
                Console.WriteLine("Enter Product Name");
                newObj.ProdName = Console.ReadLine();
                Console.Clear();
                
                Console.WriteLine("Enter a unit of measure ex. set, each, unit, pack of 10");
                newObj.ProdType = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Enter quantity of the given unit of measure");
                while (!int.TryParse(Console.ReadLine(), out quantity))
                {
                    Console.WriteLine("Enter a valid numeric quantity");
                }
                newObj.ProdQty = quantity;
                Console.Clear();
                Console.WriteLine("Enter the price per unit");
                while (!double.TryParse(Console.ReadLine(), out price))
                {
                    Console.WriteLine("Enter a valid price");
                }
                newObj.ProdPrice = price;
                Console.Clear();
                newObj.Print();
                Console.WriteLine("Product object has been logged\n\n" +
                    "[Q] to Main Menu\n" +
                    "[C] to continue another creation");
                char user;
                try
                {
                    user = char.Parse(Console.ReadLine());
                }
                catch (System.FormatException ex)
                {
                    Console.WriteLine("An error has occured:");
                    user = 'b';
                }

                while (char.ToUpper(user).Equals('C') == false && char.ToUpper(user).Equals('Q') == false)
                {
                   
                    

                        Console.WriteLine("your selection was not valid, Please provide a valid selection.");

                        user = char.Parse(Console.ReadLine());

                    
                }
                
                if (char.ToUpper(user).Equals('Q'))
                {
                    quit = true;
                }
                

                //ask if another needs to be created
            }
        }
        public static void View()
        {
            int prodId = 0;
            string name = string.Empty;
            bool end = false;
            string type = string.Empty;
            int quantity = 0;
            double price = 0;

            int result = Object.StorePrint();
            if (result != -1)
            {
                result--;
                Console.WriteLine("In order to access and modify a record, please provide a name");
                string signature = Console.ReadLine();
                Console.Clear();
                while (end == false)
                {
                    Console.WriteLine(result);
                    Console.WriteLine("Select a attribute to modify. Note: the origination can not be modified\n\n" +
                    "[I] ID\n" +
                    "[N] Name\n" +
                    "[T] Unit of measure\n" +
                    "[Q] Quantity\n" +
                    "[P] Price\n" +
                    "[E] Quit");
                    char selection = char.Parse(Console.ReadLine());

                    switch (char.ToUpper(selection))
                    {
                        case 'I':
                            Console.WriteLine("Enter a new ID");
                            prodId = int.Parse(Console.ReadLine());
                            Object.ListId.Insert(result, prodId);
                            break;
                        case 'N':
                            Console.WriteLine("Enter a new Name");
                            name = Console.ReadLine();
                            Object.ListName.Insert(result, name);
                            break;
                        case 'T':
                            Console.WriteLine("Enter a new Unit of Measure");
                            type = Console.ReadLine();
                            Object.ListType.Insert(result, type);
                            break;
                        case 'Q':
                            Console.WriteLine("Enter a new Quantity");
                            quantity = int.Parse(Console.ReadLine());
                            Object.ListQty.Insert(result, quantity);
                            break;
                        case 'P':
                            Console.WriteLine("Enter a new Price");
                            price = Convert.ToDouble(Console.ReadLine());
                            Object.ListPrice.Insert(result, price);
                            break;
                        case 'E':
                            end = true;
                            break;
                    }
                    Console.Clear();
                    Console.WriteLine(result);
                    Object.ListSignature.Insert(result, signature);
                }
            }
            else
            {
                Console.WriteLine(Object.ListSignature);
            }
            

           //ask to pick an item based on the item number
           //options to modify everything except signed
        }
    }
}
