namespace Kassasystem
{
    public class CheckIfCampaginIsActiveOnProduct
    {
        private string filePath = @".\Kampanjer";

        public string FindCampaign(string ID)
        {
            var produkt = new Produkt();
            var produktHelper = new ProductHelper();
            var campaignManagment = new CampaignManager();
            
            string updatedPrice = "";

            var products = produktHelper.ReadProductFile();
            var campaignList = produktHelper.ReadCampaignFile();

            foreach (var product in products)
            {

                foreach (var s in campaignList)
                {
                    if (product.ProduktID == s.ProductID)
                    {
                        var convertedStartDate = Convert.ToDateTime(s.CampaignStart);
                        var convertedEndDate = Convert.ToDateTime(s.CampaignEnd);

                        var isDateActive = CheckIfDateIsPresent(convertedStartDate, convertedEndDate);
                        if(isDateActive == true)
                        {
                            var newPrice = Convert.ToString(s.NewPrice);
                            updatedPrice = newPrice;
                            
                        }                        
                    }
                }
            }


          

            return updatedPrice;
        }

        private bool CheckIfDateIsPresent(DateTime campaignStartDate, DateTime campaignEndDate)
        {            
            var dateNow = DateTime.Now;

            if(dateNow >= campaignStartDate && dateNow <= campaignEndDate)
            {
                return true;
            }
            else
            {
                return false;
            }            
        } 

    }
}
