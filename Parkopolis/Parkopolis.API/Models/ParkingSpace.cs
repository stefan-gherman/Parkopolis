using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Models
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public int ParkingLotId { get; set; }
        public string Name { get; set; }
        public bool IsTaken { get; set; }
        public bool HasCarWash { get; set; }
        public bool IsCovered { get; set; }
        //TODO: talk to PO about currency and change as required
        public decimal Price { get; set; }
        public string Details { get; set; }

        //Navigation Properties

       public ParkingLot ParkingLot { get; set; }
    }
}
