using BL.Managers.Interfaces;
using CsvHelper.Configuration;
using CsvHelper;
using EFDal.Entities;
using EFDal.Repositories;
using EFDal.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FileAccessDal;

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

        //todo eric: optioneel file access kan gezien worden als een aparte DAL laag 
        public bool GenerateCSV()
        {
            var exportCsvClass = new FileAccessDal.ExportPlanningCSVClass(_repo);
            bool success = exportCsvClass.GenerateJson();
            return success;
        }
    }
}
