using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        public ICollection<Area> Areas { get; set; }
    }
}
