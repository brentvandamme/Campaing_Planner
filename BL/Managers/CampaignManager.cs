using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class CampaignManager : GenericManager<Campaign>, ICampaignManager
    {
        public CampaignManager(IGenericRepository<Campaign> repository) : base(repository)
        {
        }
    }
}
