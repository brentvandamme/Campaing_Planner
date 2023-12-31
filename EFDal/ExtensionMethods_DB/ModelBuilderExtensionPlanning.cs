﻿using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.ExtensionMethods_DB
{
    public static class ModelBuilderExtensionPlanning
    {
        public static void PlanningConfig(this ModelBuilder modelBuilder)
        {
            //entities
            modelBuilder.Entity<Planning>()
                .Property(p => p.StartVerhuur)
                .IsRequired(false);
            modelBuilder.Entity<Planning>()
                .Property(p => p.EndVerhuur)
                .IsRequired(false);

        }
    }
}
