using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Entities
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; }
        public KindOfCampaign? SoortCampagne { get; set; }
    }

    public enum KindOfCampaign
    {
        Affiche = 0,
        Folder = 1,
        Other = 2,
    }
}
