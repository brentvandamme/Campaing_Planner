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
    public class ProductManager : GenericManager<Product>, IProductManager
    {
        IProductRepositrory _productRepositrory;
        ICampaignRepository _campaignRepository;
        public ProductManager(IProductRepositrory repository, ICampaignRepository campaignRepository) : base(repository)
        {
            _productRepositrory = repository;
            _campaignRepository = campaignRepository;
        }

        public async Task<int> AddAsync(ProductAddingDto productdto) {
            Product product = new Product();

            int numberOfFreeSpots = 0;

            float price = float.Parse(productdto.Price);
            int maxCapacity = Int32.Parse(productdto.NBROfFreeSpots);
            product.Price = price;
            product.MaxAvailableCapacity = maxCapacity;
            product.Name = productdto.Name;
            //product.Campaigns = productdto.Campaigns;

            return await _productRepositrory.AddAsync(product);
        }

        public async Task<int> GetProductCapacity(int id)
        {
            //get product with id
            Product product = _productRepositrory.GetById(id);
            // get number of campaigns with that id
            int numOfCapaignsLinkedToProduct = await _campaignRepository.GetNumberOfLinkedCampaignsToProduct(id);
            //maxcap - num of campaigns --> free spots
            int currentCap = (int)(product.MaxAvailableCapacity - numOfCapaignsLinkedToProduct);

            return currentCap;
        }

        public virtual async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = _productRepositrory.GetAll();

            // Create a list of tasks to get all cpacity data at the same time
            var capacityTasks = products.Select(async item =>
            {
                item.CurrentCapacity = await GetProductCapacity(item.Id);
            });

            // Wait for all tasks to complete
            await Task.WhenAll(capacityTasks);

            return products;
        }

        public async Task<List<Product>> GetAllProductsWithFreeSpots()
        {
            List<Product> products = await GetAllProductsAsync();

            // Create a list of tasks to get all capacity data at the same time
            var capacityTasks = products.Select(async item =>
            {
                item.CurrentCapacity = await GetProductCapacity(item.Id);
            });

            // Wait for all tasks to complete
            await Task.WhenAll(capacityTasks);

            // Filter products with available capacity
            var productsWithFreeSpots = products.Where(item => item.CurrentCapacity > 0).ToList();

            return productsWithFreeSpots;
        }
    }
}
