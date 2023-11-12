using BL.Dtos;
using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class CustomerManager : GenericManager<Customer>, ICustomerManager
    {
        public CustomerManager(IGenericRepository<Customer> repository) : base(repository)
        {
        }

        public int Add(CustomerCreationDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.FirstName.Length < 1)
                throw new ArgumentException("name is too short");

            Customer customer = new Customer();
            customer.FirstName = entity.FirstName;
            customer.LastName = entity.LastName;
            customer.Company = entity.Company;

            return base.Add(customer);
        }
    }
}
