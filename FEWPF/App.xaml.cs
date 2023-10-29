using EFDal.Repositories.Interfaces;
using EFDal.Repositories;
using EFDal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace FEWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected void OnStartup(StartupEventArgs e)
        {

            var services = new ServiceCollection();
            var connectionString = "Data Source=.;Initial Catalog=PersonDB; Integrated Security=True; Trusted_Connection=True; TrustServerCertificate=True;";

            services.AddDbContext<CPDbContext>(opt => opt.UseSqlServer(connectionString), ServiceLifetime.Transient);
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<MainWindow>();

            //using ServiceProvider serviceProvider = services.BuildServiceProvider();    
            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetService<MainWindow>();

            mainWindow.InitializeComponent();
        }
    }
}
