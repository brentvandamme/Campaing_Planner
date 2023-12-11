using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.ExtensionMethods_DB
{
    public static class ModelBuilderExtensionProduct
    {
        public static void ProductConfig(this ModelBuilder modelBuilder)
        {
            //rel
            modelBuilder.Entity<Product>()
               .HasMany(p => p.Campaigns)
               .WithOne(p => p.product)
               .HasForeignKey(p => p.ProductId)
               .IsRequired(false);


            modelBuilder.Entity<PlanningProduct>()
                .HasKey(pp => new { pp.ProductId, pp.PlanningId });

            modelBuilder.Entity<PlanningProduct>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.PlanningProduct)
                .HasForeignKey(pp => pp.ProductId);

            modelBuilder.Entity<PlanningProduct>()
                .HasOne(pp => pp.Planning)
                .WithMany(pl => pl.PlanningProduct)
                .HasForeignKey(pp => pp.PlanningId);

            //properties
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired(false);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired(false);
            modelBuilder.Entity<Product>()
                .Property(p => p.MaxAvailableCapacity)
                .IsRequired(false);

        }

    }
}
