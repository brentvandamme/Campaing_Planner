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
        public ProductManager(IProductRepositrory repository) : base(repository)
        {
            _productRepositrory = repository;
        }

        public Product GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Add(ProductAddingDto productdto) {
            Product product = new Product();

            int numberOfFreeSpots = 0;
            if (!Int32.TryParse(productdto.NBROfFreeSpots, out numberOfFreeSpots))
            {
                //validatie iets??
            }

            float price = 0;
            float.TryParse(productdto.Price, out price);

            product.Price = price;
            product.MaxAvailableCapacity = numberOfFreeSpots;
            product.Name = productdto.Name;
            product.Campaigns = productdto.Campaigns;

            return _productRepositrory.Add(product);
        }
    }
}
