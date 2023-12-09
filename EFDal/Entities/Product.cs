using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Entities
{
    public class Product : BaseEntity
    {
        //todo eric: berekend veld? 
        public int MaxAvailableCapacity { get; set; }
        public List<Campaign> Campaigns { get; set; }
        public List<PlanningProduct> PlanningProduct { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
