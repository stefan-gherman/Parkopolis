using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Models
{
    public class Area
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        public  City City { get; set; }
        public ICollection<ParkingLot> ParkingLots { get; set; }
    }
}
