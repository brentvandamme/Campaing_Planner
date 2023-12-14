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

        public async Task<int> AddAsync(Planning planning, Customer cust, List<Product> products, Location loc)
        {
            planning.LastUpdate = DateTime.Now;

            //todo eric: deze voelt niet goed ;p
            //als we zeker zijn dat de customer al bestaat kunnen we zo iets doen (in principe al er een id in zit)
            // anders mag hij er zo in en gaat ef de nieuwe customer inserten
            // dan hadden we de customer (en de rest) al in de planning kunnen steken voor de aanroep van deze method
            // 
            //if (!_dbContext.ChangeTracker.Entries<Customer>().Any(e => e.Entity.Id == cust.Id))
            //{
            //    //unchanged zodat hij de customer die mogelijk enkel een id heeft niet gaat updaten
            //    _dbContext.Attach(cust).State = EntityState.Unchanged;
            //}
            //planning.Customer = cust;


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

            var existingLocation = _dbContext.Set<Location>().Local.FirstOrDefault(l => l.Id == loc.Id);
            if (existingLocation == null)
            {
                existingLocation = await _dbContext.Set<Location>().FindAsync(loc.Id);
            }

            if (existingLocation != null)
            {
                _dbContext.Attach(existingLocation);
            }
            planning.Location = existingLocation;

            //hier zou je 
            //base.Add(planning); kunnen oproepen dan moet je de lastupdate hier ook niet meer zetten
            _dbContext.Planning.Add(planning);

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);

            List<PlanningProduct> planningProducts = new List<PlanningProduct>();
            //kan normaal naar boven voor de save changes
            // als je de nieuwe in planning.PlanningProduct steekt
            // dan zou hij ze moeten mee opslaan
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
