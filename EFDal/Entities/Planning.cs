using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Entities
{
    public class Planning : BaseEntity
    {
        public DateTime? StartVerhuur { get; set; }
        public DateTime? EndVerhuur { get; set; }
        public Location? Location { get; set; }
        public List<PlanningProduct> PlanningProduct { get; set; }
        public Customer? Customer { get; set; }
    }
}
