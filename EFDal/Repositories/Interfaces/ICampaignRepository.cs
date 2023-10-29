using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Repositories.Interfaces
{
    public interface ICampaignRepository: IGenericRepository<Campaign>
    {
        List<Campaign> GetCampaignByName(string name);

        List<Campaign> GetCampaignByKind(KindOfCampaign kindOfCampaign);
    }
}
