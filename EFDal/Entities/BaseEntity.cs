﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime LastUpdate { get; set; }
         
    }
}
