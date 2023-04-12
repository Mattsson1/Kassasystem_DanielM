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


            foreach (var s in campaignList)
            {
                string[] onlyID = s.ProductID.Split('[', ']');

                if (onlyID[1] == ID)
                {
                    var convertedStartDate = Convert.ToDateTime(s.CampaignStart.Replace(" KAMPANJ SLUT", ""));
                    var convertedEndDate = Convert.ToDateTime(s.CampaignEnd);

                    var isDateActive = CheckIfDateIsPresent(convertedStartDate, convertedEndDate);
                    if (isDateActive == true)
                    {
                        var newPrice = Convert.ToString(s.NewPrice).Replace(" KAMPANJ START", "");
                        updatedPrice = newPrice;
                        break;
                    }

                }
            }


            return updatedPrice;
        }

        private bool CheckIfDateIsPresent(DateTime campaignStartDate, DateTime campaignEndDate)
        {
            var dateNow = DateTime.Now;

            if (dateNow >= campaignStartDate && dateNow <= campaignEndDate)
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
