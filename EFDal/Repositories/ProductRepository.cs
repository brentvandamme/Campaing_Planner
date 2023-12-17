using AutoMapper;
using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace EFDal.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepositrory
    {
        private readonly CPDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(CPDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await GetByIdAsync(product.Id);
            if (existingProduct != null)
            {
                _mapper.Map(product, existingProduct);

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Product with ID {product.Id} not found.");
            }
        }
    }
}
