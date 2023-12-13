﻿using AutoMapper;
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
            Product product = new Product();

            int numberOfFreeSpots = 0;

            float price = float.Parse(productdto.Price);
            int maxCapacity = productdto.MaxAvailableCapacity;
            product.Price = price;
            product.MaxAvailableCapacity = maxCapacity;
            product.Name = productdto.Name;
            //product.Campaigns = productdto.Campaigns;

            return await _productRepositrory.AddAsync(product);
        }

        public async Task<int> GetProductCapacity(int id)
        {
            Product product = await _productRepositrory.GetByIdAsync(id);

            if (product == null)
            {
                return 0;
            }

            int numOfCampaignsLinkedToProduct = await _campaignRepository.GetNumberOfLinkedCampaignsToProduct(id);
            int currentCap = 0;
            if (product.MaxAvailableCapacity >= 0)
            {
                currentCap = (int)(product.MaxAvailableCapacity - numOfCampaignsLinkedToProduct);
            }

            return currentCap;
        }


        public virtual async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products =await _productRepositrory.GetAllAsync();

            foreach (var item in products)
            {
                item.CurrentCapacity = await GetProductCapacity(item.Id);
            }

            return products;
        }

        public async Task<List<Product>> GetAllProductsWithFreeSpots()
        {
            List<Product> products = await GetAllProductsAsync();
            List<Product> productsWithCapacity = new();
            foreach (var item in products)
            {
                if (item.CurrentCapacity > 0)
                {
                    productsWithCapacity.Add(item);
                }
            }

            return productsWithCapacity;
        }

        public void Update(ProductAddingDto dto)
        {
            Product prod = _mapper.Map<Product>(dto);
            _repository.Update(prod);
        }


        public async Task UpdateAsync(ProductAddingDto dto)
        {
            Product prod = _mapper.Map<Product>(dto);
            await _productRepositrory.UpdateAsync(prod);
        }
    }
}
