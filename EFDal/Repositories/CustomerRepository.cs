using Dapper;
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
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        CPDbContext _dbContext;
        public CustomerRepository(CPDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task DeleteAsync(int customerId)
        {
            //todo eric: de connectie best met een using statment gebruiken zodat ze zeker afgesloten wordt
            string sql = "DELETE FROM Customer WHERE Id = @CustomerId";
            await _dbContext.Database.GetDbConnection().ExecuteAsync(sql, new { CustomerId = customerId });
        }

        public async Task UpdateAsync(Customer customer)
        {
            string sql = "UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Company = @Company WHERE Id = @Id";
            await _dbContext.Database.GetDbConnection().ExecuteAsync(sql, customer);
        }

    }
}
