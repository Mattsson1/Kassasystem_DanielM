using Kassasystem.Models;

namespace Kassasystem
{
    public class CampaignManager
    {
        public ProductHelper productHelper = new ProductHelper();
        private List<string> campaignListString = new List<string>();
        private List<Campaign> campaignObject = new List<Campaign>();

        private string idInput, campaignNameInput;
        private string filePath = @".\Kampanjer.txt";
        private bool isCampaignFound = false;
        private int val;
        private double newPrice;
        private DateTime campaignStartDate, campaignEndDate;

        public void CampaignSelect()
        {
            Console.Clear();
            Console.WriteLine("1. Lägg till Kampanj");
            Console.WriteLine("2. Ta bort Kampanj");
            Console.WriteLine("3. Visa alla kampanjer");
            Console.WriteLine("4. Tillbaka till menyn");
            if (int.TryParse(Console.ReadLine(), out val))
            {
                switch (val)
                {
                    case 1:
                        AddCampagin();
                        break;
                    case 2:
                        RemoveCampaign();
                        break;
                    case 3:
                        PrintAllCampaigns();
                        Console.ReadKey();
                        break;
                    case 4:
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ogiltig inmatning");
                Console.ReadKey();
            }
        }

        private List<string> PrintAllCampaigns()
        {
            campaignListString = File.ReadAllLines(filePath).ToList();

            foreach (var s in campaignListString)
            {
                Console.WriteLine(s, Console.ForegroundColor = ConsoleColor.Red);
            }
            Console.ForegroundColor = ConsoleColor.White;
            return campaignListString;
        }


        private void RemoveCampaign()
        {

            while (isCampaignFound == false)
            {
                Console.Clear();
                Console.WriteLine("Skriv in kampanj-namnet på den du vill ta bort");
                Console.WriteLine("Skriv EXIT för att återgå till meny");
                PrintAllCampaigns();

                campaignNameInput = Console.ReadLine();
                if (campaignNameInput.ToLower() == "exit") { isCampaignFound = true; break; }

                campaignObject = productHelper.ReadCampaignFile();

                foreach (var p in campaignObject)
                {
                    var correctName = p.CampaignProductName.Replace(" NYA PRISET", "");
                    if (correctName.Contains(campaignNameInput))
                    {
                        isCampaignFound = true;
                        int index = campaignObject.FindIndex(c => c.CampaignProductName.Contains(campaignNameInput));

                        campaignObject.RemoveAt(index);
                        campaignListString = productHelper.ConvertCampaignToListString(campaignObject);

                        File.WriteAllLines(filePath, campaignListString);
                        break;
                    }
                }
                if (isCampaignFound == false)
                {
                    Console.WriteLine("Hittade ingen kampanj med det angivna namnet. Testa igen ");
                    Console.ReadKey();
                }
            }
        }

        private void AddCampagin()
        {
            var produkt = new Produkt();

            Console.WriteLine("Vad ska kampanjen heta?");
            campaignNameInput = Console.ReadLine();

            Console.WriteLine("Vilken produkt vill du det ska gälla på?");
            productHelper.PrintProducts();

            idInput = Console.ReadLine();

            List<Produkt> ExistingProducts = productHelper.ReadProductFile();
            foreach (var prod in ExistingProducts)
            {
                if (idInput == prod.ProductID)
                {
                    Console.WriteLine("Ska kampanjen vara i procent eller vill du ändra hela priset?");
                    Console.WriteLine("1. Procent");
                    Console.WriteLine("2. KR");
                    double intBasePrice = Convert.ToDouble(prod.BasePrice);
                    var newPrice = UpdatePrice(intBasePrice);

                    var startAndEndDate = DateTimeInputHandler();
                    var startdate = startAndEndDate.Item1;
                    var endDate = startAndEndDate.Item2;

                    Campaign campaign = new Campaign();

                    campaignObject.Add(campaign);
                    campaignListString.Add($"ID [{idInput}]. KAMPANJ VARA: {campaignNameInput} NYA PRISET: {newPrice}" +
                    $" KAMPANJ START: {startdate.ToString("yyyy-MM-dd")} KAMPANJ SLUT: {endDate.ToString("yyyy-MM-dd")}");

                    WriteCampaignsToTextFile();
                }
            }
        }

        private void WriteCampaignsToTextFile()
        {
           
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                File.WriteAllLines(filePath, campaignListString);
            }
            else
            {
                File.AppendAllLines(filePath, campaignListString);
            }
        }

        private double UpdatePrice(double bacePrice)
        {
            while (true)
            {

                string unit = Console.ReadLine();
                if (unit == "1" || unit == "%")
                {
                    Console.WriteLine("Hur många procent ska produkten ha? Tillexempel 0.9 blir 10%");
                    double procentPrice = Convert.ToDouble(Console.ReadLine().Replace(".", ","));

                    newPrice = bacePrice * procentPrice;
                    return newPrice;
                }
                else if (unit == "2" || unit == "kr")
                {
                    Console.WriteLine("Hur mycket ska produkten kosta?");
                    int krPrice = Convert.ToInt32(Console.ReadLine());
                    newPrice = krPrice;
                    return newPrice;
                }
                else
                {
                    Console.WriteLine("Felaktigt val");
                }
            }
        }

        private (DateTime, DateTime) DateTimeInputHandler()
        {
            Console.WriteLine("När ska kampanjen börja gälla? Fyll i YYYY-MM-DD");
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out campaignStartDate))
                {
                    Console.WriteLine("När ska kampanjen ta slut?");
                    if (DateTime.TryParse(Console.ReadLine(), out campaignEndDate))
                    {
                        return (campaignStartDate, campaignEndDate);
                    }
                }
                Console.WriteLine("Fel inmatning! Fyll i YYYY-MM-DD");
            }
        }
    }
}
