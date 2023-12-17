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
    public class PlanningRepository : GenericRepository<Planning>, IPlanningRepository
    {

        private readonly CPDbContext _dbContext;

        public PlanningRepository(CPDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(Planning planning, Customer cust, List<Product> products, Location loc)
        {
            var existingCustomerEntry = _dbContext.ChangeTracker.Entries<Customer>().FirstOrDefault(e => e.Entity.Id == cust.Id);
            if (existingCustomerEntry == null)
            {
                //nog niet getracked
                _dbContext.Attach(cust).State = EntityState.Unchanged;
                planning.Customer = cust;
            }
            else
            {
                //al getracked
                var existingCustomer = existingCustomerEntry.Entity;
                planning.Customer = existingCustomer;
            }

            var existingLocationEntry = _dbContext.ChangeTracker.Entries<Location>().FirstOrDefault(e => e.Entity.Id == loc.Id);
            if (existingLocationEntry == null)
            {
                _dbContext.Attach(loc).State = EntityState.Unchanged;
                planning.Location = loc;
            }
            else
            {
                var existingLocation = existingLocationEntry.Entity;
                planning.Location = existingLocation;
            }

            List<PlanningProduct> planningProducts = new List<PlanningProduct>();
            foreach (var item in products)
            {
                PlanningProduct pp = new PlanningProduct
                {
                    PlanningId = planning.Id,
                    ProductId = item.Id
                };
                planningProducts.Add(pp);
            }
            planning.PlanningProduct= planningProducts;

            await base.AddAsync(planning);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            return planning.Id;
        }

        public List<Planning> GetAllWithIncludes()
        {
            return _dbContext.Planning
                .Include(p => p.Customer)
                .Include(p => p.PlanningProduct)
                    .ThenInclude(pp => pp.Product)
                .Include(p => p.Location)
                .ToList();
        }
        public List<Planning> GetAllWithIncludesWithCampaigns()
        {
            return _dbContext.Planning
                .Include(p => p.Customer)
                .Include(p => p.PlanningProduct)
                    .ThenInclude(pp => pp.Product)
                        .ThenInclude(p => p.Campaigns)
                .Include(p => p.Location)
                .ToList();
        }
    }
}
