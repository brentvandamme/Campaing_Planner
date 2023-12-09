using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Entities
{
    public class PlanningProduct
    {
        public int PlanningId { get; set; }
        public Planning Planning { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
