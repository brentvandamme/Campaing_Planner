using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace EFDal.Repositories.Interfaces
{
    public interface ICampaignRepository: IGenericRepository<Campaign>
    {
        //Task<int> AddAsync(Campaign campaign);

        Task<int> GetNumberOfLinkedCampaignsToProduct(int productId);
        Task<List<Campaign>> GetCampaignsByProductId(int productId);

    }
}
