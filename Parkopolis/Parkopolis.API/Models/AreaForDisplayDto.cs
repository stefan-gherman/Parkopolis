using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Models
{
    public class AreaForDisplayDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        //public ICollection<ParkingLot> ParkingLots { get; set; }
        //public int ParkingLotCount
        //{
        //    get { return ParkingLots.Count; }
        //}
       
    }
}
