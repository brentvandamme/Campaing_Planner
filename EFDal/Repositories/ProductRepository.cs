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
    public class ProductRepository : GenericRepository<Product>, IProductRepositrory
    {
        private readonly CPDbContext _dbContext;

        public ProductRepository(CPDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await GetByIdAsync(product.Id);

            if (existingProduct != null)
            {
                existingProduct.Price = product.Price;
                existingProduct.MaxAvailableCapacity = product.MaxAvailableCapacity;
                existingProduct.Name = product.Name;

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Product with ID {product.Id} not found.");
            }
        }
    }
}
