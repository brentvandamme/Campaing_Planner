using BL.Dtos;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IProductManager : IGenericManager<Product>
    {
        Task<int> AddAsync(ProductAddingDto dto);

        Task<List<Product>> GetAllProductsAsync();

        Task<List<Product>> GetAllProductsWithFreeSpots();

        //void Update(ProductAddingDto dto);

        //Task UpdateAsync(ProductAddingDto dto);
        Task<int> AddProductWithCampaignsAsync(ProductAddingDto productDto, List<Campaign> campaigns);
        Task UpdateProductWithCampaignsAsync(ProductAddingDto productDto, List<Campaign> campaigns);
    }
}
