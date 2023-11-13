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
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(CPDbContext dbContext) : base(dbContext)
        {
        }

        public List<Location> GetLocationsByCity(string city)
        {
            return _dbSet
                .Where(location => location.City == city)
                .ToList();
        }
    }
}
