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
        public CampaignRepository(CPDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetNumberOfLinkedCampaignsToProduct(int productId)
        {
            //todo eric: kan async 
            //var x = _dbSet.CountAsync(campaign => campaign.ProductId == productId);

            return await Task.Run(() =>
            {
                return _dbSet.Count(campaign => campaign.ProductId == productId);
            });
        }

    }
}
