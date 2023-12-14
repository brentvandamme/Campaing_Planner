using AutoMapper;
using BL.Dtos;
using BL.Managers.Interfaces;
using EFDal.Entities;
using EFDal.Repositories;
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
        ICustomerRepository _repo;
        public CustomerManager(ICustomerRepository repo, IMapper mapper) : base(repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public int Add(CustomerCreationDto entity)
        {
            if (entity.FirstName.Length < 1 || entity == null)
                throw new ArgumentException("name is too short");

            Customer customer = _mapper.Map<Customer>(entity);
            return base.Add(customer);
        }

        public async Task DeleteCustomerByIdAsync(int customerId)
        {
            //this is dapper
            await _repo.DeleteAsync(customerId);
            
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            //this is dapper
            await _repo.UpdateAsync(customer);
        }

    }
}
