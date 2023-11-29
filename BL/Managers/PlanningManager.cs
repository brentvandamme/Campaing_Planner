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
    public class PlanningManager : GenericManager<Planning>, IPlanningManager
    {
        IPlanningRepository _repo;
        public PlanningManager(IPlanningRepository planningRepository) : base(planningRepository) { 
        _repo= planningRepository;
        }
    }
}
