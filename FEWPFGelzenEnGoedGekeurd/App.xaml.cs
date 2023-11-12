using EFDal;
using EFDal.Repositories;
using EFDal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using BL.Managers.Interfaces;
using BL.Managers;

namespace FEWPFGelzenEnGoedGekeurd
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            string connectionString = "Data Source=.;Initial Catalog=PersonDB; " +
                                      "Integrated Security=True; " +
                                      "Trusted_Connection=True; " +
                                      "TrustServerCertificate=True;";

            // Add DbContext
            services.AddDbContext<CPDbContext>(opt => opt.UseSqlServer(connectionString), ServiceLifetime.Transient);

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
            base.OnStartup(e);

            // Retrieve MainWindow from the service provider and show it
            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }
    }
}
