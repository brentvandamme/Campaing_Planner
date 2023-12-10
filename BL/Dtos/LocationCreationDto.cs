using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class LocationCreationDto
    { 
        public string Name { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ExtraInfo { get; set; }
    }
}
