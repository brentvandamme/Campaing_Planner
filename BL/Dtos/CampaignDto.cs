using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class CampaignDto
    {
        public string name { get; set; }
        public KindOfCampaign kindOfCampaign { get; set; }
    }
}
