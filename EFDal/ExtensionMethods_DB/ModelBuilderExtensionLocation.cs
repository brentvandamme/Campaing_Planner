using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.ExtensionMethods_DB
{
    public static class ModelBuilderExtensionLocation
    {
        public static void LocationConfig(this ModelBuilder modelBuilder)
        {
            //properties
            modelBuilder.Entity<Location>()
                .Property(l => l.Name)
                .HasConversion<string>()
                .HasMaxLength(75);
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
