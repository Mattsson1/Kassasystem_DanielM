using Kassasystem.Models;

namespace Kassasystem
{
    public class ProductHelper
    {
        private string produktPath = @".\Produkt.txt";
        private string campaignPath = @".\Kampanjer.txt";

        public void PrintProducts()
        {
            List<Produkt> produkts = ReadProductFile();
            var sortedList = produkts.OrderBy(p => int.Parse(p.ProductID)).ToList();
            foreach (var s in sortedList)
            {
                Console.Write("ID:");
                Console.Write($"{s.ProductID}", Console.ForegroundColor = ConsoleColor.DarkRed);
                Console.Write($"-{s.ProductName} {s.BasePrice}{s.Unit}\n", Console.ForegroundColor = ConsoleColor.White);
            }
        }
        public List<Produkt> ReadProductFile()
        {
            if (!File.Exists(produktPath))
            {
                File.Create(produktPath).Close();               
            }

            List<string> lines = File.ReadAllLines(produktPath).ToList();
            var produkter = new List<Produkt>();

            foreach (string line in lines)
            {
                var readProdukt = new Produkt();

                string[] productArray = line.Split(".");
                readProdukt.ProductID = productArray[0];
                readProdukt.ProductName = productArray[1];
                readProdukt.BasePrice = productArray[2];
                readProdukt.Unit = productArray[3];

                produkter.Add(readProdukt);
            }
            return produkter;
        }

        public List<Campaign> ReadCampaignFile()
        {
            if (!File.Exists(campaignPath))
            {
                File.Create(campaignPath).Close();
            }

            var campaigns = new List<Campaign>();
            List<string> textCampaignList = File.ReadAllLines(campaignPath).ToList();

            foreach (var camp in textCampaignList)
            {
                var campaign = new Campaign();

                string[] campaignArray = camp.Split(":");

                campaign.ProductID = campaignArray[0];
                campaign.CampaignProductName = campaignArray[1];
                campaign.NewPrice = campaignArray[2];
                campaign.CampaignStart = campaignArray[3];
                campaign.CampaignEnd = campaignArray[4];

                campaigns.Add(campaign);
            }
            return campaigns;
        }

        public List<string> ConvertProductToListString(List<Produkt> products)
        {
            List<string> produktStrings = products.Select(s => $"{s.ProductID}.{s.ProductName}.{s.BasePrice}.{s.Unit}").ToList();
            return produktStrings;
        }

        public List<string> ConvertCampaignToListString(List<Campaign> campaigns)
        {
            List<string> campaignStrings = campaigns.Select(c => $"{c.ProductID}:{c.CampaignProductName}:{c.NewPrice}:{c.CampaignStart}:{c.CampaignEnd}").ToList();
            return campaignStrings;
        }
    }
}
