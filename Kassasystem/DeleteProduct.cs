using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
                    var nyKund = new NyKund();
                    List<Produkt> products = productHelper.ReadProductFile();
                    
                    Console.Clear();

                    Console.WriteLine("Ange ID för den produkt du vill ta bort");

                    productHelper.PrintProducts();
                    
                    val = Console.ReadLine();
                    foreach (var s in products)
                    {
                        if (val == s.ProduktID)
                        {
                            

                            products.Where(produkt => produkt.ProduktID == val).ToList().ForEach(produkt =>
                            {
                                products.Remove(produkt);
                            });

                            isProductFound = true;

                            List<string> produktStrings = productHelper.ConvertToListString(products);

                            //List<string> produktStrings = products.Select(s => $"{s.ProduktID}.{s.ProduktNamn}.{s.Pris}.{s.Enhet}").ToList();

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
