namespace Kassasystem
{
    public class NyKund
    {
        private string productID;
        private string[] products;

        private string vara, ID, amount;
        private int varor = 0;

        private double totalSumma = 0;
        private string stringSumma;
        private void ProductInput()
        {
            Console.WriteLine("Kommandon:");
            Console.WriteLine("<product id> <antal>");
            Console.WriteLine("PAY");
            Console.Write("Kommando:");

            productID = "";
            productID = Console.ReadLine().ToLower();

            vara = productID;

            products = vara.Split(" ");
        }
       
       


        public void NewCustomer()
        {
            Console.Clear();
            bool isPaying = false;
            bool isProduktOK = false;
            bool isSucces = false;

            DateTime dagensDatum = DateTime.Today;
            string dagensDatumStr = dagensDatum.ToString("yyyy-MM-dd");

            //FIXA ROOT PATH
            string receiptPath = @$".\Kvitton\RECEIPT_{dagensDatumStr}.txt";
            string folder = @".\Kvitton\";

            var produkt = new Produkt();
            var productHelper = new ProductHelper();
            List<string> kvittoLista = new List<string>();



            while (isPaying == false)
            {
                while (isProduktOK == false)
                {

                    Console.BackgroundColor = ConsoleColor.Black;

                    ProductInput();

                    List<Produkt> produkter = productHelper.ReadProductFile();

                    if (productID == "pay".ToLower())
                    {
                        isProduktOK = true;
                        break;
                    }

                    while (products.Length < 1 || products.Length > 2)//Fixa kommandon/text
                    {
                        Console.WriteLine("Du måste fylla i <produktid> och <antal>! ");

                        ProductInput();
                    }
                    if (products.Length != 2)
                    {
                        ID = products[0];
                        amount = "1";
                    }
                    else
                    {
                        ID = products[0];
                        amount = products[1];
                    }

                    bool isAmountOkConvert = Double.TryParse(amount, out double convertedAmount);
                    bool isIdOkConvert = Double.TryParse(ID, out double convertedID);
                    if (isIdOkConvert == false)
                    {
                        Console.WriteLine("ProduktID måste vara en siffra!");
                        Console.ReadKey();
                    }


                    foreach (var p in produkter)
                    {
                        if (p.ProduktID == ID)
                        {

                            //kvittoLista.Add($"***************KASSA-KVITTO*****************");
                            if (isAmountOkConvert == false)
                            {
                                Console.WriteLine("Fyll i ett giltigt antal!");
                                Console.ReadKey();
                                break;
                            }

                            varor++;
                            if (varor == 1)
                            {
                                //Convert.ToDouble(p.Pris)
                                kvittoLista.Add($"KVITTO    {p.now} \n{p.ProduktNamn} {amount} *{p.Pris}{p.Enhet} = {convertedID * convertedAmount}");
                                totalSumma += convertedID * convertedAmount;

                            }
                            else
                            {
                                kvittoLista.Add($"{p.ProduktNamn} {amount} *{p.Pris}{p.Enhet} = {convertedID * convertedAmount}");
                                totalSumma += convertedID * convertedAmount;

                            }
                            isProduktOK = true;
                            break;

                        }

                    }


                    if (isProduktOK == false && isAmountOkConvert == true && isIdOkConvert == true)
                    {
                        Console.WriteLine("Varan finns inte");
                        Console.ReadKey();
                    }


                    Console.Clear();
                    Console.WriteLine("KASSA");

                    foreach (var kvitto in kvittoLista)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(kvitto);
                    }

                    Console.WriteLine($"TOTALT: {totalSumma}");
                    isProduktOK = false;

                }
                stringSumma = Convert.ToString(totalSumma);
                kvittoLista.Add(stringSumma);

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                if (!File.Exists(receiptPath))
                {
                    File.WriteAllLines(receiptPath, kvittoLista);

                    List<string> kvittoText = File.ReadAllLines(receiptPath).ToList();

                    foreach (var file in Directory.EnumerateFiles(folder))
                    {
                        List<string> kvittoText2 = File.ReadAllLines(file).ToList();
                        foreach (var s in kvittoText2)
                        {
                            if (s.Contains("KVITTO-NUMMER:"))
                            {
                                produkt.lopNummer++;
                            }
                        }
                    }
                    kvittoLista.Add($"--------------KVITTO-NUMMER: {produkt.lopNummer}--------------");

                    File.WriteAllLines(receiptPath, kvittoLista);
                }
                else
                {
                    List<string> kvittoText = File.ReadAllLines(receiptPath).ToList();

                    foreach (var file in Directory.EnumerateFiles(folder))

                    {
                        List<string> kvittoText2 = File.ReadAllLines(file).ToList();
                        foreach (var s in kvittoText2)
                        {
                            if (s.Contains("KVITTO-NUMMER:"))
                            {
                                produkt.lopNummer++;
                            }
                        }
                    }
                    kvittoLista.Add($"--------------KVITTO-NUMMER: {produkt.lopNummer}--------------\n");
                    File.AppendAllLines(receiptPath, kvittoLista);

                }
                isPaying = true;
                Console.BackgroundColor = ConsoleColor.Black;
                break;


            }
        }
    }
}