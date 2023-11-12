using EFDal.Entities;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=CampaignPlanner;Integrated Security=True; Trusted_Connection=True; TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(Asse)

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Campaigns);

            modelBuilder.Entity<Planning>()
                .HasMany(p => p.Products);

            modelBuilder.Entity<Planning>()
                .HasOne(l => l.Location);

            modelBuilder.Entity<Planning>()
                .HasOne(k => k.Customer);

            modelBuilder.Entity<Location>()
                .Property(l => l.City)
                .HasConversion<string>()
                .HasMaxLength(59);
            modelBuilder.Entity<Location>()
                .Property(l => l.Zip)
                .HasConversion<string>()
                .HasMaxLength(25);
            modelBuilder.Entity<Location>()
                .Property(l => l.Number)
                .HasConversion<string>()
                .HasMaxLength(10);
            modelBuilder.Entity<Location>()
                .Property(l => l.ExtraInfo)
                .HasConversion<string>()
                .HasMaxLength(500);
        }
    }
}
