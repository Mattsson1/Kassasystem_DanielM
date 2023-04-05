using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystem
{
    public class Campaign
    {
        
        public string CampaignID { get; set; }
        public string CampaignProductName { get; set; }
        public double NewPrice { get; set; }
        public DateTime CampaignStart { get; set; }
        public DateTime CampaignEnd { get; set; }

   
        public Campaign(string campaignId, string campaignProductName, double newPrice, DateTime campaignStart, DateTime campaignEnd)
        {
            CampaignID = campaignId;
            CampaignProductName = campaignProductName;
            NewPrice = newPrice;
            CampaignStart = campaignStart;
            CampaignEnd = campaignEnd;
        }
    }
}
