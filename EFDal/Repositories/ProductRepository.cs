using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepositrory
    {
        public ProductRepository(CPDbContext dbContext) : base(dbContext)
        {
        }

        public List<Product> GetProductsWithFreeSpots()
        {
            return _dbSet
               .Where(product => product.NBROfFreeSpots != 0)
               .ToList();
        }
    }
}
