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

        CPDbContext _context;
        public CampaignRepository(CPDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> GetNumberOfLinkedCampaignsToProduct(int productId)
        {
            return await Task.Run(() =>
            {
                return _dbSet.Count(campaign => campaign.ProductId == productId);
            });
        }
        public async Task<List<Campaign>> GetCampaignsByProductId(int productId)
        {
            return await _context.Set<Campaign>()
                .Where(c => c.ProductId == productId)
                .ToListAsync();
        }

    }
}
