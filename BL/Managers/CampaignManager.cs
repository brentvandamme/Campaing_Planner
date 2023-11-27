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
    public class CampaignManager : GenericManager<Campaign>, ICampaignManager
    {
        public CampaignManager(IGenericRepository<Campaign> repository) : base(repository)
        {
        }
        public int Add(CampaignDto entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.name.Length < 1)
                throw new ArgumentException("name is too short");

            Campaign campaign= new Campaign();
            campaign.Name = entity.name;
            campaign.SoortCampagne = entity.kindOfCampaign;


            return base.Add(campaign);
        }
    }
}
