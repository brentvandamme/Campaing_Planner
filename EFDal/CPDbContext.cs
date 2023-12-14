using EFDal.Entities;
using EFDal.ExtensionMethods_DB;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace EFDal
{
    public class CPDbContext : DbContext
    {
        public CPDbContext() : base()
        {

        }
        public CPDbContext(DbContextOptions<CPDbContext> options) : base(options) //***
        {

        }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Planning> Planning { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PlanningProduct> PlanningProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=CampaignPlanner;Integrated Security=True; Trusted_Connection=True; TrustServerCertificate=True;")
                    .EnableSensitiveDataLogging();
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //todo eric: gebruik van extention methods:
            //modelBuilder.ProductConfig();
            ModelBuilderExtensionProduct.ProductConfig(modelBuilder);
            ModelBuilderExtensionCampaign.CampaignConfig(modelBuilder);
            ModelBuilderExtensionCustomer.CustomerConfig(modelBuilder);
            ModelBuilderExtensionPlanning.PlanningConfig(modelBuilder);
            ModelBuilderExtensionLocation.LocationConfig(modelBuilder);

        }
    }
}
