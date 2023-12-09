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

        //public async Task<int> AddAsync(Planning planning)
        //{
        //    planning.LastUpdate = DateTime.Now;
        //    _dbContext.Planning.Add(planning);
        //    await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        //    return planning.Id;
        //}

        public async Task<IEnumerable<Planning>> GetAllAsync()
        {
            return await _dbContext.Planning.ToListAsync();
        }

        public async Task<int> AddAsync(Planning planning, Customer cust)
        {
            planning.LastUpdate = DateTime.Now;
            _dbContext.Attach(cust);
            planning.Customer = cust;
            _dbContext.Planning.Add(planning);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return planning.Id;
        }
        public List<Planning> GetAllWithIncludes()
        {
            return _dbContext.Planning
                .Include(p => p.Customer)
                .Include(p => p.PlanningProduct)
                .Include(p => p.Location)
                .ToList();
        }
    }
}
