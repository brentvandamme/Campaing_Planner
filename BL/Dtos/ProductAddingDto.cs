using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class ProductAddingDto
    {
        public int MaxAvailableCapacity { get; set; }
        //public List<Campaign> Campaigns { get; set; }
        public string Price { get; set; }
        public string Name { get; set; }
        public int Id { get; set; } 
    }
}

