using AutoMapper;
using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
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

        public async Task<List<Product>> GetAllProductsWithCapacity()
        {
            string sql = @"
                SELECT p.Id,
                    p.MaxAvailableCapacity,
                    p.Price,
                     p.Name,
                    p.LastUpdate,
                    COUNT(c.Id) AS InUse,
                    (p.MaxAvailableCapacity - COUNT(c.Id)) AS CurrentCapacity
                    FROM Product p
                    LEFT JOIN Campaigns c ON c.ProductId = p.Id
                    GROUP BY p.Id, p.MaxAvailableCapacity, p.Price, p.Name, p.LastUpdate;
                    ";

            using (var connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<Product>(sql);

                return result.ToList();
            }
        }

        public async Task<List<Product>> GetAllProductsWithFreeCapacity()
        {
            string sql = @"
                SELECT p.Id,
                    p.MaxAvailableCapacity,
                    p.Price,
                     p.Name,
                    p.LastUpdate,
                    COUNT(c.Id) AS InUse,
                    (p.MaxAvailableCapacity - COUNT(c.Id)) AS CurrentCapacity
                    FROM Product p
                    LEFT JOIN Campaigns c ON c.ProductId = p.Id
                    GROUP BY p.Id, p.MaxAvailableCapacity, p.Price, p.Name, p.LastUpdate
                    HAVING (p.MaxAvailableCapacity - COUNT(c.Id)) > 0;
                    ";

            using (var connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<Product>(sql);

                return result.ToList();
            }
        }

        public async Task<int> GetProductCapacityById(int productId)
        {
            string sql = @"
                SELECT p.Id,
                    p.MaxAvailableCapacity,
                    p.Price,
                    p.Name,
                    p.LastUpdate,
                    COUNT(c.Id) AS InUse,
                    (p.MaxAvailableCapacity - COUNT(c.Id)) AS Available

                    FROM Product p
                    JOIN Campaigns c ON c.ProductId = p.Id AND p.Id = @ProductId
                    GROUP BY p.Id, p.MaxAvailableCapacity, p.Price, p.Name, p.LastUpdate;
                     ";


            using (var connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<int>(sql, new { ProductId = productId });
                return result;
            }
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
