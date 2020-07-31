using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Models
{
    public class AreaDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }  
    }
}
