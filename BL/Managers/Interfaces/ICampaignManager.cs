﻿using BL.Dtos;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface ICampaignManager : IGenericManager<Campaign>
    {
        int Add(CampaignDto campaignDto);
        Task<int> AddAsync(CampaignDto campaignDto);
        Task<List<Campaign>> GetCampaignsByProductId(int productid);
        //Task<List<Campaign>> GetAllCampaignsAsync();
        Task<List<Campaign>> GetCampaignsByProductIdAsync(int productId);
    }
}
