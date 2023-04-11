namespace Kassasystem
{
    public class CheckIfCampaginIsActiveOnProduct
    {
        private string filePath = @".\Kampanjer";
        
        public void FindCampaign(string ID)
        {
            var produkt = new Produkt();
            var produktHelper = new ProductHelper();
            var campaignManagment = new CampaignManager();

            var products = produktHelper.ReadProductFile();

            List<string> campaignList = File.ReadAllLines(filePath).ToList();

            foreach(var product in products)
            {
                foreach(var campaign in campaignList)
                {
                    
                }
            }


        }



    }
}
