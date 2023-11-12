using EFDal;
using EFDal.Repositories;
using EFDal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using BL.Managers.Interfaces;
using BL.Managers;
using Microsoft.Data.SqlClient;

namespace FEWPFGelzenEnGoedGekeurd
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            string connectionString = "Data Source=.\\SQLEXPRESS;" +
                                        "Initial Catalog=CampaignPlanner;" +
                                        "Integrated Security=True; " +
                                        "Trusted_Connection=True; " +
                                        "TrustServerCertificate=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }


            // Add DbContext
            services.AddDbContext<CPDbContext>(opt => opt.UseSqlServer(connectionString));

            // Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            // Managers
            services.AddTransient(typeof(IGenericManager<>), typeof(GenericManager<>));
            services.AddTransient<ICustomerManager, CustomerManager>();

            // Windows
            services.AddSingleton<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);

            // Retrieve MainWindow from the service provider and show it
            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }
    }
}
