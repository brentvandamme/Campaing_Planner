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

        public List<Planning> GetPlanningByTimeSpan(DateTime startTime, DateTime endTime)
        {
            return _dbSet
                .Where(planning => planning.StartVerhuur >= startTime && planning.EndVerhuur <= endTime)
                .ToList();
        }

        public async Task<IEnumerable<Planning>> GetAllAsync()
        {
            return await _dbContext.Planning.ToListAsync();
        }

        public async Task<int> AddAsync(Planning planning, Customer cust, List<Product> products)
        {
            planning.LastUpdate = DateTime.Now;

            var existingCustomer = _dbContext.Set<Customer>().Local.FirstOrDefault(c => c.Id == cust.Id);

            if (existingCustomer == null)
            {
                existingCustomer = await _dbContext.Set<Customer>().FindAsync(cust.Id);
            }

            if (existingCustomer != null)
            {
                _dbContext.Attach(existingCustomer);
            }
            planning.Customer = existingCustomer;

            _dbContext.Planning.Add(planning);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

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
            _dbContext.PlanningProduct.AddRange(planningProducts);

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
    }
}
