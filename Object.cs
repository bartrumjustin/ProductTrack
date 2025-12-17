using ConsoleTables;



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
                $"| Price:{ProdPrice:C} Onhand Value:{(ProdQty * ProdPrice):C}\n" +
                $"--------------------------------------------------\n" +
                $"Originated by {GetSigned()}");
            Console.ForegroundColor = ConsoleColor.White;
            ListId.Insert(Index, ProdId);
            ListName.Insert(Index, ProdName);
            ListType.Insert(Index, ProdType);
            ListQty.Insert(Index, ProdQty);
            ListPrice.Insert(Index, ProdPrice);
            ListSignature.Insert(Index, GetSigned());
            Index++;

        }
        public static int StorePrint()
        {
            int output = 0;
            int i = 0;

            bool end = false;


            var table = new ConsoleTable("Record", "ID", "Name", "Quantity", "U/M", "Price", "Total", "Last Authored");
            // Console.WriteLine($"{Index}");

            if (Index == 0)
            {
                Console.WriteLine("There is nothing to report, please create records to view");
            }
            else
            {
                for (i = 0; i < Index; i++)
                {
                    table.AddRow((i + 1), ListId[i], ListName[i], ListQty[i], ListType[i], ListPrice[i], $"{((ListQty[i]) * (ListPrice[i])):C}", ListSignature[i]);
                }

                table.Write();
                Console.WriteLine($"Make a selection by entering the [record #] in order to change\n" +
                            $"[Q] to quit and back to Menu\n");

                char selection;
                selection = char.Parse(Console.ReadLine());

                while (char.ToUpper(selection).Equals('Q') == false)
                {
                    output = selection - '0';
                    if (output < 1 || output > Index + 1)
                    {
                        Console.WriteLine("Your record selection was out of range, Select a valid record number");
                        selection = char.Parse(Console.ReadLine());
                    }
                    else
                    {
                        break;
                    }
                }


            }
            return output;

        }
        public static void Alter(int result, int prodId, string name, string type, int quantity, double price, string signature)
        {
            if (prodId == 0)
            {
                prodId = ListId[result];
            }
            if (quantity == 0)
            {
                quantity = ListQty[result];
            }
            if (price == 0)
            {
                price = ListPrice[result];
            }
            if (name == string.Empty)
            {
                name = ListName[result];
            }
            if (type == string.Empty)
            {
                type = ListType[result];
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                 "--------------------------------------------------\n" +
                $"|     Number: {prodId}       Name: {name}     \n" +
                $"|                OnHand: {quantity} / {type}   \n" +
                $"| Price:{price:C} Onhand Value:{(quantity * price):C}\n" +
                $"--------------------------------------------------\n" +
                $"Originated by {signature}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Please Confirm: [Y] to save changes or [N] to discard");
            char selection = char.Parse(Console.ReadLine());
            while (char.ToUpper(selection).Equals('Y') == false && char.ToUpper(selection).Equals('N') == false)
            {
                Console.WriteLine("Your selection must be [Y] or [N]");
                selection = char.Parse(Console.ReadLine());
            }
            if (char.ToUpper(selection).Equals('Y'))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                     "--------------------------------------------------\n" +
                    $"|     Number: {prodId}       Name: {name}     \n" +
                    $"|                OnHand: {quantity} / {type}   \n" +
                    $"| Price:{price:C} Onhand Value:{(quantity * price):C}\n" +
                    $"--------------------------------------------------\n" +
                    $"Originated by {signature}");
                Console.ForegroundColor = ConsoleColor.White;
                ListId.Insert(result, prodId);
                ListName.Insert(result, name);
                ListType.Insert(result, type);
                ListQty.Insert(result, quantity);
                ListPrice.Insert(result, price);
                ListSignature.Insert(result, signature);
                Console.WriteLine("The record is now modified and recorded [enter to continue]");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                     "--------------------------------------------------\n" +
                    $"|     Number: {prodId}       Name: {name}     \n" +
                    $"|                OnHand: {quantity} / {type}   \n" +
                    $"| Price:{price:C} Onhand Value:{(quantity * price):C}\n" +
                    $"--------------------------------------------------\n" +
                    $"Originated by {signature}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Your changes have been discarded, returning back to view list [enter to continue]");
                Console.ReadKey();
            }
        }


    }
}
