using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.ExtensionMethods_DB
{
    public static class ModelBuilderExtensionCampaign
    {
        public static void CampaignConfig(this ModelBuilder modelBuilder)
        {
            //properties
            modelBuilder.Entity<Campaign>()
                .Property(p => p.Name)
                .HasMaxLength(100);
            modelBuilder.Entity<Campaign>()
                .Property(p => p.SoortCampagne)
                .HasConversion<String>()
                .HasMaxLength(50);

        }
    }
}
