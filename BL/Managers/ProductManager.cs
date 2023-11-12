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
    }
}
