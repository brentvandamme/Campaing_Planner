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
        //List<Product> GetProductsWithFreeSpots();
        Task UpdateAsync(Product product);

    }
}
