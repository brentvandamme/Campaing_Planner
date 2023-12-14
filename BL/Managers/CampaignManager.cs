using AutoMapper;
using BL.Dtos;
using BL.Managers.Interfaces;
using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using EFDal.Repositories;
using EFDal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class CampaignManager : GenericManager<Campaign>, ICampaignManager
    {
        ICampaignRepository _repo;
        private readonly IMapper _mapper;
        public CampaignManager(ICampaignRepository repository, IMapper mapper) : base(repository)
        {
            _repo = repository;
            _mapper = mapper;
        }
        public int Add(CampaignDto campaignDto)
        {
            if (campaignDto == null || campaignDto.Name.Length < 1)
                throw new ArgumentNullException(nameof(campaignDto));

            Campaign campaign = _mapper.Map<Campaign>(campaignDto);

            return _repo.Add(campaign);
        }

        public async Task<int> AddAsync(CampaignDto campaignDto)
        {
            //todo eric: hier dan geen validatie?
            Campaign campaign = _mapper.Map<Campaign>(campaignDto);

            int index = await _repo.AddAsync(campaign);
            return index;
        }

        public async Task<List<Campaign>> GetCampaignsByProductId(int productId)
        {
            //todo eric: beter om in de db te filteren
            //nu haal je heel de lijst uit de db om er dan hier een uit te filteren
            var campaignsList = _repo
                .GetAll()
                .Where(campaign => campaign.ProductId == productId)
                .ToList();

            return campaignsList;
        }

    }
}
