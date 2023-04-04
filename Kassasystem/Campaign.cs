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
        public double NewPrice { get; set; }
        public DateTime CampaignStart { get; set; }
        public DateTime CampaignEnd { get; set; }

   
        public Campaign(string campaignId, double newPrice, DateTime campaignStart, DateTime campaignEnd)
        {
            CampaignID = campaignId;
            NewPrice = newPrice;
            CampaignStart = campaignStart;
            CampaignEnd = campaignEnd;
        }
    }
}
