using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.ExtensionMethods_DB
{
    public static class ModelBuilderExtensionCustomer
    {
        public static void CustomerConfig(this ModelBuilder modelBuilder)
        {
            //properties
            modelBuilder.Entity<Customer>()
                .Property(p => p.FirstName)
                .HasMaxLength(100)
                .IsRequired(false);
            modelBuilder.Entity<Customer>()
                .Property(p => p.LastName)
                .HasMaxLength(100)
                .IsRequired(false);
            modelBuilder.Entity<Customer>()
                .Property(p => p.Company)
                .HasMaxLength(100)
                .IsRequired(false);
        }
    }
}
