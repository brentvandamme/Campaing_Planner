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
        public CustomerRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public List<Customer> GetCustomersByName(string name)
        {
            return _dbSet
                .Where(customer => customer.FirstName == name)
                .ToList();
        }

        public List<Customer> GetCustomersByLastName(string lastName)
        {
            return _dbSet
                .Where(customer => customer.LastName == lastName)
                .ToList();
        }

        public List<Customer> GetCustomersByCompany(string company)
        {
            return _dbSet
                .Where(customer => customer.Company == company)
                .ToList();
        }
    }
}
