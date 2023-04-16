using Kassasystem.Models;

namespace Kassasystem
{
    public class DeleteProduct
    {
        private string val;
        private bool isProductFound = false;

        public void Delete()
        {
            string produktPath = @".\Produkt.txt";

            while (isProductFound == false)
            {
                try
                {
                    var productHelper = new ProductHelper();
                    var nyKund = new CashRegister();
                    List<Produkt> products = productHelper.ReadProductFile();

                    Console.Clear();
                    Console.WriteLine("Ange ID för den produkt du vill ta bort");

                    productHelper.PrintProducts();

                    val = Console.ReadLine();
                    foreach (var s in products)
                    {
                        if (val == s.ProduktID)
                        {
                            isProductFound = true;

                            products.RemoveAll(product => val.Contains(product.ProduktID));

                            List<string> produktStrings = productHelper.ConvertProductToListString(products);

                            File.WriteAllLines(produktPath, produktStrings);
                            Console.WriteLine($"{s.ProduktNamn} Bortagen från kassan");
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
