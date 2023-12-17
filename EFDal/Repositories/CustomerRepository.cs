using Dapper;
using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        CPDbContext _dbContext;
        public CustomerRepository(CPDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task DeleteAsync(int customerId)
        {
            //delete related plannings
            var planningsToDelete = await _dbContext.Planning
                .Where(p => p.Customer.Id == customerId)
                .ToListAsync();

            _dbContext.Planning.RemoveRange(planningsToDelete);
            await _dbContext.SaveChangesAsync();

            //delete customers
            string sql = "DELETE FROM Customer WHERE Id = @CustomerId";
            using (var connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, new { CustomerId = customerId });
            }

            // Save changes to persist deletions
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            string sql = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Company = @Company WHERE Id = @Id";

            using (var connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                await connection.OpenAsync();

                await _dbContext.Database.GetDbConnection().ExecuteAsync(sql, customer);
            }
        }

    }
}
