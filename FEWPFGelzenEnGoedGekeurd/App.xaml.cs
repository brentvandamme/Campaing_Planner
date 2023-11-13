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

            // Add DbContext
            services.AddDbContext<CPDbContext>(opt => opt.UseSqlServer(connectionString));

            // Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            // Managers
            services.AddTransient(typeof(IGenericManager<>), typeof(GenericManager<>));
            services.AddTransient<ICustomerManager, CustomerManager>();

            // Register the MainWindow with injected ICustomerRepository
            services.AddSingleton<MainWindow>();

            ServiceProvider = services.BuildServiceProvider();

            // Explicitly create and show the MainWindow
            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}