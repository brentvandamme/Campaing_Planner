using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Repositories
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public List<Campaign> GetCampaignByKind(KindOfCampaign kindOfCampaign)
        {
            List<Campaign> result = _dbSet.Where(p => p.SoortCampagne == kindOfCampaign).ToList();
            return result;
        }

        public List<Campaign> GetCampaignByName(string name)
        {
            List<Campaign> result = _dbSet.Where(p => p.Name == name).ToList();
            return result;
        }
    }
}
