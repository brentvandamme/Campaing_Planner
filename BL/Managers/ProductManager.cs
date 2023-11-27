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
        public ProductManager(IGenericRepository<Product> repository) : base(repository)
        {
        }

        public Product GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public int Add(ProductAddingDto productdto) {
            Product product = new Product();

            int numberOfFreeSpots = 0;
            Int32.TryParse(productdto.NBROfFreeSpots, out numberOfFreeSpots);

            float price = 0;
            float.TryParse(productdto.Price, out price);

            product.Price = price;
            product.NBROfFreeSpots= numberOfFreeSpots;
            product.Name = productdto.Name;
            product.Campaigns = productdto.Campaigns;

            return base.Add(product);
        }
    }
}
