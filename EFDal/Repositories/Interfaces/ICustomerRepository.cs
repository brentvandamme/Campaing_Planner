using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomersByName(string name);
        List<Customer> GetCustomersByLastName(string lastName);
        List<Customer> GetCustomersByCompany(string lastName);
    }
}
