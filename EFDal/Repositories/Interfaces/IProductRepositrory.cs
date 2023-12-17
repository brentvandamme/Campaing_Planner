using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Repositories.Interfaces
{
    public interface IProductRepositrory : IGenericRepository<Product>
    {
        Task UpdateAsync(Product product);

        Task<int> GetProductCapacityById(int productId);

        Task<List<Product>> GetAllProductsWithCapacity();

        Task<List<Product>> GetAllProductsWithFreeCapacity();

    }
}
