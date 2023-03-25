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

            try
            {
                var nyKund = new NyKund();
                List<Produkt> products = nyKund.ReadProductFile();
                List<string> newList = File.ReadAllLines(produktPath).ToList();


                Console.Clear();

                Console.WriteLine("Ange ID för den produkt du vill ta bort");
                foreach (var s in products)
                {
                    Console.Write($"{s.ProduktID}: ");
                    Console.Write($"{s.ProduktNamn}\n");

                }

                val = Console.ReadLine();
                foreach(var s in products)
                {      
                    if (val == s.ProduktID)
                    {
                        products.Where(produkt => produkt.ProduktID == val).ToList().ForEach(produkt =>
                        {
                            products.Remove(produkt);
                        });
                            
                        
                        isProductFound = true;

                        File.WriteAllLines(produktPath,products);
                        break;
                    }
                }
                if(isProductFound == false)
                {
                    Console.WriteLine("Finns inget ID som matchar");
                }

                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ogiltig inmatnig {ex}");
            }
        }


    }
}
