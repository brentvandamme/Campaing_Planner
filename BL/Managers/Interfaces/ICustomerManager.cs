﻿using BL.Dtos;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface ICustomerManager : IGenericManager<Customer>
    {
        int Add(CustomerCreationDto customerCreation);
        Task DeleteCustomerByIdAsync(int customerId);

        Task UpdateCustomerAsync(Customer customer);
    }
}
