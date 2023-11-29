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
    public class CampaignManager : GenericManager<Campaign>, ICampaignManager
    {
        ICampaignRepository _repo;
        public CampaignManager(ICampaignRepository repository) : base(repository)
        {
            _repo = repository;
        }
        public int Add(CampaignDto entity)
        {
            //todo eric: alle validatie in 1 keer als er 2 probs zijn wil ik het niet 2 * moeten uitvoeren
            if (entity == null || entity.Name.Length < 1)
                throw new ArgumentNullException(nameof(entity));

            //todo eric: automapper
            Campaign campaign = new Campaign();
            campaign.Name = entity.Name;
            campaign.SoortCampagne = entity.SoortCampagne;
            campaign.LastUpdate = DateTime.Now;

            
            return _repo.Add(campaign);
        }

        public async Task<int> AddAsync(CampaignDto campaignDto)
        {
            //todo eric: automapper
            Campaign campaign = new Campaign();
            campaign.Name = campaignDto.Name;
            campaign.SoortCampagne = campaignDto.SoortCampagne;
            campaign.LastUpdate = DateTime.Now;

            int index = await _repo.AddAsync(campaign);
            return index;
        }

    }
}
