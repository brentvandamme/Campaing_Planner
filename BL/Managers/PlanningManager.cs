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
    public class PlanningManager : GenericManager<Planning>, IPlanningManager
    {
        IPlanningRepository _repo;
        public PlanningManager(IPlanningRepository planningRepository) : base(planningRepository)
         { 
            _repo= planningRepository;
        }
        public async Task AddAsync(Planning planning, Customer customer, List<Product> Product, Location loc)
        {
            await _repo.AddAsync(planning, customer, Product, loc);
        }

        public List<Planning> GetAllWithIncludes()
        {
            return _repo.GetAllWithIncludes();
        }
    }
}
