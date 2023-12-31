﻿using EFDal.Entities;
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
            modelBuilder.ProductConfig();
            modelBuilder.CampaignConfig();
            modelBuilder.CustomerConfig();
            modelBuilder.PlanningConfig();
            modelBuilder.LocationConfig();
        }
    }
}
