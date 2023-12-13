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

        //public List<Product> GetProductsWithFreeSpots()
        //{
        //    return _dbSet
        //       .Where(product => product.MaxAvailableCapacity != 0)
        //       .ToList();
        //}
        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await GetByIdAsync(product.Id);

            if (existingProduct != null)
            {
                // Update properties of the existing product
                existingProduct.Price = product.Price;
                existingProduct.MaxAvailableCapacity = product.MaxAvailableCapacity;
                existingProduct.Name = product.Name;

                // Update other properties as needed

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Product with ID {product.Id} not found.");
            }
        }
    }
}
