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
    public class ProductManager : GenericManager<Product>, IProductManager
    {
        IProductRepositrory _productRepositrory;
        ICampaignRepository _campaignRepository;
        IMapper _mapper;
        public ProductManager(IProductRepositrory repository, ICampaignRepository campaignRepository, IMapper mapper) : base(repository)
        {
            _productRepositrory = repository;
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(ProductAddingDto productdto) {
            Product product = _mapper.Map<Product>(productdto);

            return await _productRepositrory.AddAsync(product);
        }

        public async Task<int> GetProductCapacity(int id)
        {
            int capacity = await _productRepositrory.GetProductCapacityById(id);

            return capacity;
        }


        public virtual async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products =await _productRepositrory.GetAllProductsWithCapacity();

            return products;
        }

        public async Task<List<Product>> GetAllProductsWithFreeSpots()
        {
            List<Product> prodwithcap = await _productRepositrory.GetAllProductsWithFreeCapacity();

            return prodwithcap;
        }

        public async Task<int> AddProductWithCampaignsAsync(ProductAddingDto productDto, List<Campaign> campaigns)
        {
            Product product = _mapper.Map<Product>(productDto);

            int productId = await _productRepositrory.AddAsync(product);

            if (productId != 0)
            {
                var newProduct = await GetByIdAsync(productId);

                foreach (var campaign in campaigns)
                {
                    campaign.product = newProduct;
                    campaign.ProductId = newProduct.Id;
                    await _campaignRepository.UpdateAsync(campaign);
                }
            }

            return productId;
        }
        public async Task UpdateProductWithCampaignsAsync(ProductAddingDto productDto, List<Campaign> campaigns)
        {
            Product prod = _mapper.Map<Product>(productDto);
            await _productRepositrory.UpdateAsync(prod);

            Product newProduct = await GetByIdAsync(productDto.Id);

            foreach (var campaign in campaigns)
            {
                campaign.product = newProduct;
                campaign.ProductId = newProduct.Id;
                await _campaignRepository.UpdateAsync(campaign);
            }
        }

    }
}
