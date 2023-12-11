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
using System.Configuration;
using BL.Mappingprofiles;

namespace FEWPFGelzenEnGoedGekeurd
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            string connectionString = FEWPFGelzenEnGoedGekeurd.Properties.Settings.Default.Connectionstring;
            //string connectionString = "Data Source=.\\SQLEXPRESS;" +
            ////string connectionString = "Data Source=.;" +
            //                           "Initial Catalog=CampaignPlanner;" +
            //                           "Integrated Security=True; " +
            //                           "Trusted_Connection=True; " +
            //                           "TrustServerCertificate=True;";

            // Add DbContext
            services.AddDbContext<CPDbContext>(opt => opt.UseSqlServer(connectionString),ServiceLifetime.Transient);
            services.AddAutoMapper(typeof(MappingProfile));
            // Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICampaignRepository, CampaignRepository>();
            services.AddTransient<IProductRepositrory, ProductRepository>();
            services.AddTransient<IPlanningRepository, PlanningRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();

            // Managers
            services.AddTransient(typeof(IGenericManager<>), typeof(GenericManager<>));
            services.AddTransient<ICustomerManager, CustomerManager>();
            services.AddTransient<ICampaignManager, CampaignManager>();
            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<IPlanningManager, PlanningManager>();
            services.AddTransient<ILocationManager, LocationManager>();

            // Register the MainWindow with injected ICustomerRepository
            services.AddSingleton<MainWindow>();
            services.AddTransient<CampaignWindow>();
            services.AddTransient<ProductWindow>();
            services.AddTransient<PlannerWindow>();
            services.AddTransient<LocationWindow>();

            ServiceProvider = services.BuildServiceProvider();

            // Explicitly create and show the MainWindow
            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}