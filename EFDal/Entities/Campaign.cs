﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Entities
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; }
        public KindOfCampaign? SoortCampagne { get; set; }

        public int? ProductId { get; set; }
        public Product? product { get; set; }
    }
}
