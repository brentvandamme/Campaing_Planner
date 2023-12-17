using CsvHelper;
using CsvHelper.Configuration;
using EFDal.Repositories.Interfaces;
using System.Formats.Asn1;
using System.Globalization;
using System.Text.Json;

namespace FileAccessDal
{
    public class ExportPlanningCSVClass
    {
        private readonly IPlanningRepository _repo;

        public ExportPlanningCSVClass(IPlanningRepository repo)
        {
            _repo = repo;
        }

        //DEPRECATED
        //public bool GenerateCSV()
        //{
        //    //todo eric: complexe structuren kan je beter naar json serializen/deserializen en opslaan ipv csv
        //    var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //    var filePath = Path.Combine(desktopPath, "planning_data.csv");

        //    var allWithIncludes = _repo.GetAllWithIncludesWithCampaigns();

        //    try
        //    {

        //        using (var writer = new StreamWriter(filePath))
        //        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        //        {
        //            // Manually write header
        //            csv.WriteField("start verhuur");
        //            csv.WriteField("einde verhuur");

        //            csv.WriteField("product name");
        //            csv.WriteField("product MaxAvailableCapacity");
        //            csv.WriteField("product related campagnes");

        //            csv.WriteField("location name");
        //            csv.WriteField("location city");
        //            csv.WriteField("location zip");
        //            csv.WriteField("location street");
        //            csv.WriteField("location number");
        //            csv.WriteField("location extra info");

        //            csv.WriteField("customer first name");
        //            csv.WriteField("customer last name");
        //            csv.WriteField("customer company");
        //            // Add more fields as needed...

        //            // End the header line
        //            csv.NextRecord();

        //            // Manually write records
        //            //todo eric:
        //            //csv.WriteRecords(allWithIncludes);?
        //            foreach (var planning in allWithIncludes)
        //            {
        //                csv.WriteField(planning?.StartVerhuur);
        //                csv.WriteField(planning?.EndVerhuur);

        //                //todo eric: gaat waarschijnlijk mis gaan als er meer dan 1 product is(csv is een platte tabel)
        //                foreach (var planningProduct in planning.PlanningProduct)
        //                {
        //                    csv.WriteField(planningProduct.Product?.Name);
        //                    csv.WriteField(planningProduct.Product?.MaxAvailableCapacity);

        //                    var campaignNames = planningProduct.Product?.Campaigns.Select(c => c.Name);
        //                    var campaignsField = campaignNames != null ? string.Join(", ", campaignNames) : null;
        //                    csv.WriteField(campaignsField);
        //                }

        //                csv.WriteField(planning.Location?.Name);
        //                csv.WriteField(planning.Location?.City);
        //                csv.WriteField(planning.Location?.Zip);
        //                csv.WriteField(planning.Location?.Street);
        //                csv.WriteField(planning.Location?.Number);
        //                csv.WriteField(planning.Location?.ExtraInfo);

        //                csv.WriteField(planning.Customer?.FirstName);
        //                csv.WriteField(planning.Customer?.LastName);
        //                csv.WriteField(planning.Customer?.Company);

        //                csv.NextRecord();
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        return false;
        //    }
        //}

        public bool GenerateJson()
        {
           
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var filePath = Path.Combine(desktopPath, "planning_data.json");

                var allWithIncludes = _repo.GetAllWithIncludesWithCampaigns();

                try
                {
                    using (var writer = new StreamWriter(filePath))
                    {
                        foreach (var planning in allWithIncludes)
                        {
                            var planningData = new
                            {
                                StartVerhuur = planning?.StartVerhuur,
                                EindeVerhuur = planning?.EndVerhuur,
                                Product = planning?.PlanningProduct
                                    .Select(pp => new
                                    {
                                        Name = pp.Product?.Name,
                                        MaxAvailableCapacity = pp.Product?.MaxAvailableCapacity,
                                        Campaigns = pp.Product?.Campaigns.Select(c => c.Name).ToArray()
                                    })
                                    .ToArray(),
                                Location = new
                                {
                                    Name = planning?.Location?.Name,
                                    City = planning?.Location?.City,
                                    Zip = planning?.Location?.Zip,
                                    Street = planning?.Location?.Street,
                                    Number = planning?.Location?.Number,
                                    ExtraInfo = planning?.Location?.ExtraInfo
                                },
                                Customer = new
                                {
                                    FirstName = planning?.Customer?.FirstName,
                                    LastName = planning?.Customer?.LastName,
                                    Company = planning?.Customer?.Company
                                }
                            };

                            var jsonData = JsonSerializer.Serialize(planningData, new JsonSerializerOptions
                            {
                                WriteIndented = true
                            });

                            writer.WriteLine(jsonData);
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
