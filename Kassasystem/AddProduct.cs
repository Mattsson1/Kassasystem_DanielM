namespace Kassasystem
{
    public class AddProduct
    {
        private string ProduktID, ProduktNamn, Pris, Enhet;
        private int lineCounter = 0;
        private int val, idInput;

        public void AdminCase1Add()
        {
            bool isIdNotOk = true;
            bool okProdukt = false;
            string produktPath = @".\Produkt.txt";
            var productHelper = new ProductHelper();

            List<Produkt> produkts = productHelper.ReadProductFile();
            List<string> produktAdds = new List<string>();
            List<int> idList = new List<int>();

            while (okProdukt == false)
            {
                try
                {
                    while (isIdNotOk == true)
                    {
                        Console.Clear();
                        Console.WriteLine("Fyll i ID på produkten");
                        Console.WriteLine("Dessa IDn är upptagna");

                        productHelper.PrintProducts();

                        foreach (var s in produkts)
                        {
                            int idINT;
                            idList.Add(idINT = Convert.ToInt32(s.ProduktID));
                        }

                        idInput = Convert.ToInt32(Console.ReadLine());

                        isIdNotOk = idList.Any(id => id == idInput);

                        if (idInput > 1000 || idInput < 0)
                        {
                            Console.WriteLine("Id måste vara mellan 0 till 1000");
                            Console.ReadKey();
                            isIdNotOk = true;
                        }
                        else if (isIdNotOk == true)
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

                    Console.Clear();
                    Console.WriteLine("Vilken Pris enhet vill du ha på produkten?");
                    Console.WriteLine("1. KR/ST");
                    Console.WriteLine("2. KR/KG");

                    bool isUnitOk = false;
                    while (isUnitOk == false)
                    {
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
                            }                            
                        }
                        else
                        {
                            Console.WriteLine("Du måste ange siffror och välja mellan de två alternativen");
                        }
                    }
                    Console.WriteLine("Ange hur mycket produkten ska kosta");
                    Pris = Console.ReadLine();

                    produktAdds.Add($"{idInput}.{ProduktNamn}. {Pris.Replace(".", ",")}. {Enhet}");

                    File.AppendAllLines(produktPath, produktAdds);

                    okProdukt = true;

                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex} Ogiltig inmatning");
                    Console.ReadKey();
                }
            }
        }
    }
}