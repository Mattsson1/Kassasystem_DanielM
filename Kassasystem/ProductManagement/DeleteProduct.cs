using Kassasystem.Models;

namespace Kassasystem
{
    public class DeleteProduct
    {
        private string val;
        private bool isProductFound = false;
        private string produktPath = @".\Produkt.txt";
        private List<Produkt> products = new List<Produkt>();
        public void Delete()
        {            
            while (isProductFound == false)
            {
                try
                {
                    var productHelper = new ProductHelper();
                    var nyKund = new CashRegister();
                    products = productHelper.ReadProductFile();

                    Console.Clear();
                    Console.WriteLine("Ange ID för den produkt du vill ta bort");

                    productHelper.PrintProducts();

                    val = Console.ReadLine();
                    if(val.ToLower() == "exit") { break; }
                    foreach (var s in products)
                    {
                        if (val == s.ProductID)
                        {
                            isProductFound = true;

                            products.RemoveAll(product => val.Contains(product.ProductID));

                            List<string> produktStrings = productHelper.ConvertProductToListString(products);

                            File.WriteAllLines(produktPath, produktStrings);
                            Console.WriteLine($"{s.ProductName} Bortagen från kassan");
                            break;
                        }
                    }
                    if (isProductFound == false)
                    {
                        Console.WriteLine("Finns inget ID som matchar");
                    }
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ogiltig inmatnig {ex}");
                }
            }
        }
    }
}
