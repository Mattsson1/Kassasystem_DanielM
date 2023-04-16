
namespace Kassasystem
{
    public class CashRegister
    {
        CheckIfCampaginIsActiveOnProduct findCampaign = new CheckIfCampaginIsActiveOnProduct();
        Produkt produkt = new Produkt();
        ProductHelper productHelper = new ProductHelper();

        private string cashRegisterInput;
        private string[] productsArray;
        private string folder = @".\Kvitton\";
        private string product, ID, amount,price;
        private int amountOfProducts = 0;

        private List<Produkt> produkter;
        private List<string> kvittoLista = new List<string>();
        List<string> kvittoText = new List<string>();

        private bool isPaying, isProductOK, isAmountOkConvert, isIdOkConvert = false;

        private double moneyTotal = 0, convertedPrice;
        private string moneyTotalInString;

        public void NewCustomer()
        {
            ResetValues();            

            Console.Clear();
            isPaying = false;
            isProductOK = false;

            productHelper.ReadProductFile();

            while (isPaying == false || cashRegisterInput.ToUpper() == "EXIT")
            {
                while (isProductOK == false)
                {
                    Console.BackgroundColor = ConsoleColor.Black;

                    ProductInput();

                    if (cashRegisterInput == "pay".ToLower())//CHECKOUT
                    {
                        isProductOK = true;
                        break;
                    }
                    while (productsArray.Length < 1 || productsArray.Length > 2)//Fixa kommandon/text
                    {
                        Console.WriteLine("Du måste fylla i <produktid> och <antal>! ");
                        ProductInput();
                    }
                    if (productsArray.Length != 2)
                    {
                        ID = productsArray[0];
                        amount = "1";
                    }
                    else
                    {
                        ID = productsArray[0];
                        amount = productsArray[1];
                    }

                    isAmountOkConvert = Double.TryParse(amount, out double convertedAmount);
                    isIdOkConvert = Double.TryParse(ID, out double convertedID);

                    if (isAmountOkConvert == false)
                    {
                        Console.WriteLine("Fyll i ett giltigt antal!");
                        Console.ReadKey();
                    }
                    if (isIdOkConvert == false)
                    {
                        Console.WriteLine("ProduktID måste vara en siffra!");
                        Console.ReadKey();
                    }
                    if (isAmountOkConvert == true && isIdOkConvert == true)
                    {
                        LookForProduct(convertedAmount);
                    }

                    if (isProductOK == false && isAmountOkConvert == true && isIdOkConvert == true)
                    {
                        Console.WriteLine("Varan finns inte");
                        Console.ReadKey();
                    }

                    PrintReceipt();

                    isProductOK = false;

                }
                moneyTotalInString = Convert.ToString(moneyTotal);
                kvittoLista.Add(moneyTotalInString);

                WriteToTextFile();

                Console.BackgroundColor = ConsoleColor.Black;

                isPaying = true;
                break;
            }
        }

        private void ResetValues()
        {
            kvittoLista = new List<string>();
            kvittoText = new List<string>();
            produkt.lopNummer = 0;
            convertedPrice = 0;
            amountOfProducts = 0;
            moneyTotal = 0;
            price = "0";
        }

        private void PrintReceipt()
        {
            Console.Clear();
            Console.WriteLine("KASSA");

            foreach (var kvitto in kvittoLista)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(kvitto);
            }

            Console.WriteLine($"TOTALT: {moneyTotal}");
        }

        private void WriteToTextFile()
        {
            DateTime dagensDatum = DateTime.Today;
            string dagensDatumStr = dagensDatum.ToString("yyyy-MM-dd");

            //FIXA ROOT PATH
            string receiptPath = @$".\Kvitton\RECEIPT_{dagensDatumStr}.txt";

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (!File.Exists(receiptPath))
            {
                File.WriteAllLines(receiptPath, kvittoLista);                

                foreach (var file in Directory.EnumerateFiles(folder))
                {
                    kvittoText = File.ReadAllLines(file).ToList();
                    foreach (var s in kvittoText)
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
                foreach (var file in Directory.EnumerateFiles(folder))

                {
                    kvittoText = File.ReadAllLines(file).ToList();
                    foreach (var s in kvittoText)
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
        }

        private void ProductInput()
        {
            Console.WriteLine("Kommandon:");
            Console.WriteLine("<product id> <antal>");
            Console.WriteLine("PAY");
            Console.Write("Kommando:");

            cashRegisterInput = "";
            cashRegisterInput = Console.ReadLine().ToLower();

            product = cashRegisterInput;
            productsArray = product.Split(" ");
        }


        private void LookForProduct(double convertedAmount)
        {
            produkter = productHelper.ReadProductFile();
            foreach (var p in produkter)//GÖRA OM TILL FUNKTION?
            {
                if (p.ProduktID == ID)
                {
                    price = findCampaign.FindCampaign(ID, p.BasePrice);
                    bool isPriceOkConvert = Double.TryParse(price, out convertedPrice);//FIXA KONVERTERINGEN

                    amountOfProducts++;
                    if (amountOfProducts == 1)
                    {
                        kvittoLista.Add($"KVITTO    {p.now} \n{p.ProduktNamn} {amount} *{price}{p.Enhet} = {convertedPrice * convertedAmount}");
                        moneyTotal += convertedPrice * convertedAmount;
                    }
                    else
                    {
                        kvittoLista.Add($"{p.ProduktNamn} {amount} *{price}{p.Enhet} = {convertedPrice * convertedAmount}");
                        moneyTotal += convertedPrice * convertedAmount;
                    }
                    isProductOK = true;
                    break;
                }
            }
        }
    }
}