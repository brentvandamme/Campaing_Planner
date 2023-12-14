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

        public bool GenerateCSV()
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, "planning_data.csv");

            var allWithIncludes = _repo.GetAllWithIncludesWithCampaigns();

            try
            {
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    // Manually write header
                    csv.WriteField("start verhuur");
                    csv.WriteField("einde verhuur");

                    csv.WriteField("product name");
                    csv.WriteField("product MaxAvailableCapacity");
                    csv.WriteField("product related campagnes");

                    csv.WriteField("location name");
                    csv.WriteField("location city");
                    csv.WriteField("location zip");
                    csv.WriteField("location street");
                    csv.WriteField("location number");
                    csv.WriteField("location extra info");

                    csv.WriteField("customer first name");
                    csv.WriteField("customer last name");
                    csv.WriteField("customer company");
                    // Add more fields as needed...

                    // End the header line
                    csv.NextRecord();

                    // Manually write records
                    foreach (var planning in allWithIncludes)
                    {
                        csv.WriteField(planning?.StartVerhuur);
                        csv.WriteField(planning?.EndVerhuur);

                        foreach (var planningProduct in planning.PlanningProduct)
                        {
                            csv.WriteField(planningProduct.Product?.Name);
                            csv.WriteField(planningProduct.Product?.MaxAvailableCapacity);

                            var campaignNames = planningProduct.Product?.Campaigns.Select(c => c.Name);
                            var campaignsField = campaignNames != null ? string.Join(", ", campaignNames) : null;
                            csv.WriteField(campaignsField);
                        }

                        csv.WriteField(planning.Location?.Name);
                        csv.WriteField(planning.Location?.City);
                        csv.WriteField(planning.Location?.Zip);
                        csv.WriteField(planning.Location?.Street);
                        csv.WriteField(planning.Location?.Number);
                        csv.WriteField(planning.Location?.ExtraInfo);

                        csv.WriteField(planning.Customer?.FirstName);
                        csv.WriteField(planning.Customer?.LastName);
                        csv.WriteField(planning.Customer?.Company);

                        csv.NextRecord();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }
    }
}
