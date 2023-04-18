
using Kassasystem.Models;

namespace Kassasystem
{
    public class AddProduct
    {
        private List<Produkt> produkts;
        private List<string> produktAdds = new List<string>();
        private List<int> idList = new List<int>();

        ProductHelper productHelper = new ProductHelper();

        private string ProduktNamn, Pris, Enhet;
        private string produktPath = @".\Produkt.txt";
        private int val, idInput;
        private bool isIdOk, okProdukt, isUnitOk = false;

        public void AdminCase1Add()
        {
            produkts = productHelper.ReadProductFile();

            while (okProdukt == false)
            {
                try
                {
                    while (isIdOk == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Fyll i ID på produkten");
                        Console.WriteLine("Dessa IDn är upptagna");

                        productHelper.PrintProducts();

                        foreach (var s in produkts)
                        {
                            int idINT;
                            idList.Add(idINT = Convert.ToInt32(s.ProductID));
                        }

                        idInput = Convert.ToInt32(Console.ReadLine());
                        isIdOk = idList.Any(id => id == idInput);

                        if (idInput > 1000 || idInput < 0)
                        {
                            Console.WriteLine("Id måste vara mellan 0 till 1000");
                            Console.ReadKey();

                        }
                        else if (isIdOk == true)
                        {
                            Console.WriteLine("Id finns redan! välj ett annat!");
                            Console.ReadKey();
                        }
                        else if (idInput < 1000 || idInput > 0)
                        {
                            break;
                        }
                    }

                    Console.WriteLine("Ange namnet på produkten");
                    ProduktNamn = Console.ReadLine();

                    GetPriceUnit();

                    Console.WriteLine("Ange hur mycket produkten ska kosta");
                    Pris = Console.ReadLine();

                    produktAdds.Add($"{idInput}.{ProduktNamn}. {Pris.Replace(".", ",")}. {Enhet}");

                    File.AppendAllLines(produktPath, produktAdds);

                    Console.WriteLine($"Produkt {ProduktNamn} tillagd!");
                    okProdukt = true;
                    Console.ReadKey();
                }
                catch (Exception)
                {
                    Console.WriteLine($"Ogiltig inmatning");
                    Console.ReadKey();
                }
            }
        }

        public string GetPriceUnit()
        {
            isUnitOk = false;
            while (isUnitOk == false)            
            {
                Console.WriteLine("Vilken Pris enhet vill du ha på produkten?");
                Console.WriteLine("1. KR/ST");
                Console.WriteLine("2. KR/KG");

                if (int.TryParse(Console.ReadLine(), out val))
                {
                    if (val == 1)
                    {
                        Enhet = "kr/st";
                        isUnitOk = true;
                        break;
                    }
                    if (val == 2)
                    {
                        Enhet = "kr/kg";
                        isUnitOk = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Välj mellan de två alternativen");
                        Console.ReadKey();                    
                    }
                }
                else
                {
                    Console.WriteLine("Du måste ange siffror och välja mellan de två alternativen");
                    Console.ReadKey();
                }
                
            }
            return Enhet;
        }
    }
}