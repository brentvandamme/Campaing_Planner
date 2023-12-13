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
            // Get the number of campaigns where campaign.productid = productId
            return await _dbSet
                .Where(campaign => campaign.ProductId == productId)
                .CountAsync();
        }

    }
}
