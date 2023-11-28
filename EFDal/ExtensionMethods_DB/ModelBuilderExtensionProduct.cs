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


            modelBuilder.Entity<Product>()
                .HasMany(p => p.plannings);

            //properties
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(100);


        }

    }
}
