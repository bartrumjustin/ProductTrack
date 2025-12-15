using System;
using System.Data.SqlTypes;
using System.Runtime.InteropServices.Marshalling;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using System.Data.Common;



namespace FinalProject
{

    public class Object
    {
        public static int Index = 0;
        public static List<int> ListId = new List<int>();
        public static List<int> ListQty = new List<int>();
        public static List<double> ListPrice = new List<double>();
        public static List<string> ListName = new List<string>();
        public static List<string> ListType = new List<string>();
        public static List<string> ListSignature = new List<string>();
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        
        public string ProdType { get; set; }
        public int ProdQty { get; set; }
        public double ProdPrice { get; set; }
        private string _Signed;
        public string GetSigned()
        { 
            return _Signed;
        }
        public void SetSigned(string signature)
            {
                while (string.IsNullOrWhiteSpace(signature) || signature.Contains(" ") || signature.Any(char.IsDigit))
                {
                    Console.WriteLine("The entry is not a recognized name, try agian");
                signature = Console.ReadLine();
                }
                Console.WriteLine("Name now logged and will be associated with made records.");
                _Signed = signature;
            }
        
        


        public Object()
        {
            
            ProdId = 0;
            ProdName = string.Empty;
            
            ProdType = string.Empty;
            ProdQty = 0;
            ProdPrice = 0;
            _Signed = string.Empty;
            

        }
        public Object(int prodId, string name, string type, int quantity, double price, string signature)
        {
            ProdId = prodId;
            ProdName = name;
            
            ProdType = type;
            ProdQty = quantity;
            ProdPrice = price;
            _Signed = signature;
        }

        

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(
                 "--------------------------------------------------\n" +
                $"|     Number: {ProdId}       Name: {ProdName}     \n" +
                $"|                OnHand: {ProdQty} / {ProdType}   \n" +
                $"| Price:{ProdPrice:C} Onhand Value:{(ProdQty*ProdPrice):C}\n" +
                $"--------------------------------------------------\n" +
                $"Originated by {GetSigned()}");
            Console.ForegroundColor = ConsoleColor.White;
            ListId.Insert(Index,ProdId);
            ListName.Insert(Index,ProdName);
            ListType.Insert(Index,ProdType);
            ListQty.Insert(Index,ProdQty);
            ListPrice.Insert(Index,ProdPrice);
            ListSignature.Insert(Index, GetSigned());
            Index++;

        }
        public static int StorePrint()
        {   
            int output = 0;
            int i = 0;
            int page = 3;
            bool end = false;
            string cont = "[C] to continue";
            // Console.WriteLine($"{Index}");
            if (Index == 0)
            {
                Console.WriteLine("There is nothing to report, please create records to view");
            }
            else
            {


                while (end == false)
                {

                    for (i = i; i < page; i++)
                    {
                        if (i == Index)
                        {
                            page = Index;
                            Console.WriteLine("no more left");
                            break;
                        }
                        else
                        {
                            string message =
                                $"--------------------------------------------------\n" +
                                $"********************Record# {(i + 1)}*******************\n" +
                                 "--------------------------------------------------\n" +
                                $"|     Number: {ListId[i]}       Name: {ListName[i]}     \n" +
                                $"|                OnHand: {ListQty[i]} / {ListType[i]}   \n" +
                                $"| Price:{(ListPrice[i]):C} Onhand Value:{((ListQty[i]) * (ListPrice[i])):C}\n" +
                                $"--------------------------------------------------\n" +
                                $"Originated by {ListSignature[i]}";
                            Console.WriteLine($"{i}, {Index}, {page}");
                            Console.WriteLine(message);
                        }
                    }
                    Console.WriteLine($"You are viewing records {(i - 2)} to {page} of {Index}\n\n" +
                    $"Make a selection by entering the [record #] in order to change\n" +
                    $"[Q] to quit and back to Menu\n");
                    if (i < Index)
                    {
                        Console.WriteLine(cont);
                    }
                    else
                    {
                        end = true;
                    }
                    char selection;
                    selection = char.Parse(Console.ReadLine());
                    if (char.IsNumber(selection))
                    {
                        output = selection - '0';
                        end = true;
                        break;

                    }
                    while (char.ToUpper(selection).Equals('C') == false && char.ToUpper(selection).Equals('Q') == false)
                    {
                        Console.WriteLine("your selection was not valid, Please provide a valid selection.");

                        selection = char.Parse(Console.ReadLine());
                    }

                    if (char.ToUpper(selection).Equals('Q'))
                    {
                        end = true;
                        output = -1;
                    }
                    else
                    {
                        i = page;
                        page += 3;
                    }

                }
                
            }
            return output;

        }
       

    }
}
