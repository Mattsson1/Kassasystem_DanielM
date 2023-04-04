namespace Kassasystem
{
    public partial class AdminConsoleApp
    {
        public class UpdateProduct
        {

            public void Update()
            {
                var productHelper = new ProductHelper();
                string produktPath = @".\Produkt.txt";
                List<Produkt> produkts = productHelper.ReadProductFile();
                Console.WriteLine("Ange ID för produkten du vill ändra");
                productHelper.PrintProducts();

                int val;

                if (int.TryParse(Console.ReadLine(), out val))
                {
                    string valUpt = Convert.ToString(val);
                    foreach (var s in produkts)
                    {
                        if (valUpt == s.ProduktID)
                        {
                            Console.WriteLine("Vad vill du ändra på produkten?");
                            Console.WriteLine("1. Namn");
                            Console.WriteLine("2. Pris/enhet");
                            Console.WriteLine("3. Kampanj");
                            Console.WriteLine("4. Redigera allt från början");

                            if (int.TryParse(Console.ReadLine(), out val))
                            {
                                if (val == 1)
                                {
                                    Console.WriteLine($"Vad ska produkten ändra namn till? Nuvarande namn är {s.ProduktNamn}");
                                    string newName = Console.ReadLine();
                                    s.ProduktNamn = newName;
                                    
                                }
                                if (val == 2)
                                {
                                    Console.WriteLine("Vilken pris-enhet ska produkten ha?");
                                    string newUnit = Console.ReadLine();
                                    Console.WriteLine($"Vad ska produkten ändra pris till? Nuvarande pris är {s.BasePrice}");
                                    string newPrice = Console.ReadLine();
                                    s.BasePrice = newPrice.Replace(".",",");
                                    s.Enhet = newUnit;

                                }
                                if (val == 3)
                                {
                                    //Kampanj ändring
                                    Console.WriteLine($"");
                                    
                                }
                                if (val == 4) { }

                                List<string> produktStrings = productHelper.ConvertToListString(produkts);
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
                Console.ReadKey();
            }
        }
    }
}
