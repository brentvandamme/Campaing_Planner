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
        public PlanningRepository(CPDbContext dbContext) : base(dbContext)
        {
        }

        public List<Planning> GetPlanningByTimeSpan(DateTime startTime, DateTime endTime)
        {
            return _dbSet
                .Where(planning => planning.StartVerhuur >= startTime && planning.EndVerhuur <= endTime)
                .ToList();
        }
    }
}
