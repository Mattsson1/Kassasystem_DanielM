namespace Kassasystem
{
    public class CheckIfCampaginIsActiveOnProduct
    {
        private bool isCampaignFound = false;

        public string FindCampaign(string ID, string basePrice)
        {
            var produktHelper = new ProductHelper();
            var campaignList = produktHelper.ReadCampaignFile();

            string updatedPrice = "";                       

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
                        isCampaignFound = true;
                        break;
                    }

                }
            }
            if(isCampaignFound == false)
            {
                updatedPrice = basePrice;
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
