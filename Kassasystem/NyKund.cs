using static Kassasystem.Program;
using static System.Net.Mime.MediaTypeNames;

namespace Kassasystem
{

    internal partial class Program
    {
        public class NyKund
        {

            

            public void NewCustomer()
            {
                DateTime dagensDatum = DateTime.Today;
                string dagensDatumStr = dagensDatum.ToString("yyyy-MM-dd");


                string produktPath = "C:\\Users\\Gamer\\Documents\\GitHub\\Kassasystem_DanielM\\Kassasystem\\Produkt.txt";
                string receiptPath = $"C:\\Users\\Gamer\\Documents\\GitHub\\Kassasystem_DanielM\\Kassasystem\\Kvitton\\RECEIPT_{dagensDatumStr}.txt";
                var produkt = new Produkt();

                var produkter = new List<Produkt>();

                List<string> kvittoLista = new List<string>();
                List<string> lines = File.ReadAllLines(produktPath).ToList();//Retunerar en string array

                foreach (string line in lines)
                {
                    var readProdukt = new Produkt();

                    string[] strings = line.Split(".");
                    readProdukt.ProduktID = strings[0];
                    readProdukt.ProduktNamn = strings[1];
                    readProdukt.Pris = strings[2];
                    readProdukt.Enhet = strings[3];
                   
                    produkter.Add(readProdukt);
                }

                string productID;
                string vara;
                string ID;
                string amount;
                int varor = 0;
                double totalSumma = 0;
                string stringSumma;

                bool cashier = true;

                while (cashier == true)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("KASSA");
                    Console.WriteLine("Kommandon");
                    Console.WriteLine("<product id> <antal>");

                        productID = Console.ReadLine();
                        vara = productID;

                        string[] products = vara.Split(" ");

                        while (products.Length < 1 || products.Length > 2)
                        {
                            Console.WriteLine("Du måste fylla i <produktid> och <antal>! ");
                            Console.ReadKey();
                            productID = Console.ReadLine();
                            vara = productID;

                            products = vara.Split(" ");
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
           
                    foreach (var p in produkter)
                    {
                        if (p.ProduktID == ID)
                        {
                            kvittoLista.Add($"***************KASSA-KVITTO*****************");

                            varor++;
                            if (varor == 1)
                            {
                                kvittoLista.Add($"{p.now} \n{p.ProduktNamn} {amount}st *{p.Pris} = {Convert.ToDouble(p.Pris) * Convert.ToDouble(amount)}");
                                totalSumma += Convert.ToDouble(p.Pris) * Convert.ToDouble(amount);
                            }
                            else
                            {
                                kvittoLista.Add($"{p.ProduktNamn} {amount}st *{p.Pris} = {Convert.ToDouble(p.Pris) * Convert.ToInt32(amount)}");
                                totalSumma += Convert.ToDouble(p.Pris) * Convert.ToDouble(amount);
                                
                            }
                        }
                    }

                    Console.Clear();

                    Console.WriteLine("KVITTO");
                    foreach (var kvitto in kvittoLista)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(kvitto);

                        
                    }

                        
                    Console.WriteLine($"TOTALT: {totalSumma}");

                    if (productID == "pay".ToLower())
                    {
                        stringSumma = Convert.ToString(totalSumma);
                        kvittoLista.Add(stringSumma);


                        if (!File.Exists(receiptPath))
                        {
                            kvittoLista.Add($"--------------KVITTO-NUMMER: {produkt.lopNummer}--------------");

                            File.WriteAllLines(receiptPath, kvittoLista);
                            
                        }
                        else
                        {
                            List<string> kvittoText = File.ReadAllLines(receiptPath).ToList();
                            foreach (var s in kvittoText)
                            {
                                if (s.Contains("KVITTO-NUMMER:"))
                                {
                                    produkt.lopNummer++;
                                    
                                    
                                }
                            }

                            kvittoLista.Add($"--------------KVITTO-NUMMER: {produkt.lopNummer}--------------\n");

                            File.AppendAllLines(receiptPath, kvittoLista);


                        }

                        cashier = false;
                        
                    }
                }
            }
        }
    }
}