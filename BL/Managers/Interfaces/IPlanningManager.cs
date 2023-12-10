using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IPlanningManager : IGenericManager<Planning>
    {
       Task AddAsync(Planning planning, Customer customer, List<Product> product);

        List<Planning> GetAllWithIncludes();
    }
}
