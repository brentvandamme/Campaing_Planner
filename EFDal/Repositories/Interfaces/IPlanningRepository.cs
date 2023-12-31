﻿using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Repositories.Interfaces
{
    public interface IPlanningRepository : IGenericRepository<Planning>
    {

        Task<int> AddAsync(Planning planning, Customer cust, List<Product> Product, Location loc);

        List<Planning> GetAllWithIncludes();
        List<Planning> GetAllWithIncludesWithCampaigns();
    }
}
