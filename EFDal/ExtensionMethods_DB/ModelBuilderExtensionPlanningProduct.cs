using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.ExtensionMethods_DB
{
    public static class ModelBuilderExtensionPlanningProduct
    {
        public static void PlanningProductConfig(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanningProduct>()
                .HasKey(pp => new { pp.PlanningId, pp.ProductId });

            modelBuilder.Entity<PlanningProduct>()
                .HasOne(pp => pp.Planning)
                .WithMany(p => p.PlanningProduct)
                .HasForeignKey(pp => pp.PlanningId);

            modelBuilder.Entity<PlanningProduct>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.PlanningProduct)
                .HasForeignKey(pp => pp.ProductId);

        }
    }
}
