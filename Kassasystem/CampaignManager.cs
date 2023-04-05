﻿using Microsoft.VisualBasic;

namespace Kassasystem
{
    public partial class AdminConsoleApp
    {
        public class CampaignManager
        {


            private List<string> campaignListString = new List<string>();
            private List<Campaign> campaignObject = new List<Campaign>();
            private string idInput;
            private int val;
            private double newPrice;
            private DateTime campaignStartDate, campaignEndDate;
            
            private string filePath = @".\Kampanjer";
            public void CampaignSelect()
            {
                Console.WriteLine("1. Lägg till Kampanj");
                Console.WriteLine("2. Ta bort Kampanj");
                Console.WriteLine("3. Redigera Kampanj");

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
                            EditCampaign();
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

            private void EditCampaign()
            {
                var produkt = new Produkt();
                var productHelper = new ProductHelper();
                List<Produkt> ExistingProducts = productHelper.ReadProductFile();
                foreach (var prod in ExistingProducts)
                {
                    DateTimeInputHandler();
                }
            }

            private void RemoveCampaign()
            {
                throw new NotImplementedException();
            }

            private void AddCampagin()
            {
                
                var produkt = new Produkt();
                var productHelper = new ProductHelper();

                Console.WriteLine("Vilken produkt vill du det ska gälla på?");
                productHelper.PrintProducts();

                idInput = Console.ReadLine();

                List<Produkt> ExistingProducts = productHelper.ReadProductFile();
                foreach (var prod in ExistingProducts)
                {
                    if (idInput == prod.ProduktID)
                    {
                        Console.WriteLine("Ska kampanjen vara i procent eller vill du ändra hela priset?");
                        Console.WriteLine("1. Procent");
                        Console.WriteLine("2. KR");
                        double intBasePrice = Convert.ToDouble(prod.BasePrice);
                        UpdatePrice(intBasePrice);

                        DateTimeInputHandler();

                        Campaign campaign = new Campaign(prod.ProduktID,prod.ProduktNamn, newPrice, campaignStartDate, campaignEndDate);
                        
                        campaignObject.Add(campaign);
                        campaignListString = campaignObject.Select(obj => $"ID:{obj.CampaignID} KAMPANJ VARA: {obj.CampaignProductName} NYA PRISET: {obj.NewPrice} KAMPANJ START: {obj.CampaignStart.ToString("yyyy-MM-dd")} KAMPANJ SLUT: {obj.CampaignEnd.ToString("yyyy-MM-dd")}").ToList();

                        WriteCampaignsToTextFile();
                    }
                }              


                //produkt.campaigns.Add();
                //produkt.date = new DateTime();
            }

            private void WriteCampaignsToTextFile()
            {              


                if (!File.Exists(filePath))
                {
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
                        double procentPrice = Convert.ToDouble(Console.ReadLine().Replace(".",","));
                        
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
                Console.WriteLine("När ska kampanjen börja gälla?");

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
}
