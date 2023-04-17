using Kassasystem.Models;
using System.Runtime.CompilerServices;

namespace Kassasystem
{
    public partial class AdminConsoleApp
    {
        public class UpdateProduct
        {
            private int val;
            private bool isExiting = false;
            private List<Produkt> produkts;
            private string produktPath = @".\Produkt.txt";
            ProductHelper productHelper = new ProductHelper();
            AddProduct addProduct = new AddProduct();
            public void Update()
            {               
                produkts = productHelper.ReadProductFile();                                             

                while (isExiting == false)
                {
                    Console.Clear();
                    Console.WriteLine("Ange ID för produkten du vill ändra");
                    productHelper.PrintProducts();
                    SearchForProduct();
                    
                    Console.ReadKey();
                }
            }

            private void SearchForProduct()
            {
                if (int.TryParse(Console.ReadLine(), out val))
                {
                    string valUpt = Convert.ToString(val);
                    foreach (var s in produkts)
                    {
                        if (valUpt == s.ProductID)
                        {                            
                            Console.WriteLine("Vad vill du ändra på produkten?");
                            Console.WriteLine("1. Namn");
                            Console.WriteLine("2. Pris/enhet");
                            Console.WriteLine("3. Tillbaka till meny");

                            if (int.TryParse(Console.ReadLine(), out val))
                            {
                                if (val == 1)
                                {
                                    var newName = ChangeName(s.ProductName);                                   
                                    s.ProductName = newName;

                                }
                                if (val == 2)
                                {
                                    var newPriceUnit = addProduct.GetPriceUnit();

                                    Console.WriteLine($"Vad ska produkten ändra pris till? Nuvarande pris är {s.BasePrice}");
                                    string newPrice = Console.ReadLine();
                                    s.BasePrice = newPrice.Replace(".", ",");
                                    s.Unit = newPriceUnit;

                                }
                                if (val == 3)
                                {
                                    isExiting = true;
                                    break;
                                }
                                List<string> produktStrings = productHelper.ConvertProductToListString(produkts);
                                File.WriteAllLines(produktPath, produktStrings);
                            }
                            else
                            {
                                Console.WriteLine("Välj mellan de fyra alternativen");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Ange siffra");
                }
            }

            private string ChangeName(string oldName)
            {
                Console.WriteLine($"Vad ska produkten ändra namn till? Nuvarande namn är {oldName}");
                string newName = Console.ReadLine();
                Console.WriteLine($"Produkten har ändrat namn FRÅN: {oldName} TILL: {newName}");

                return newName;
            }
        }
    }
}
