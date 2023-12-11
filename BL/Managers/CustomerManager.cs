using AutoMapper;
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
        private readonly IMapper _mapper;
        public CustomerManager(ICustomerRepository repo, IMapper mapper) : base(repo)
        {
            _mapper = mapper;
        }

        public int Add(CustomerCreationDto entity)
        {
            if (entity.FirstName.Length < 1 || entity == null)
                throw new ArgumentException("name is too short");

            Customer customer = _mapper.Map<Customer>(entity);
            //customer.FirstName = entity.FirstName;
            //customer.LastName = entity.LastName;
            //customer.Company = entity.Company;

            return base.Add(customer);
        }
    }
}
