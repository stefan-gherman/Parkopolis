using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.MockData
{
    public class ParkingLotsDataStore
    {
        public static ParkingLotsDataStore CurrentParkingLots { get; } = new ParkingLotsDataStore();
        public List<ParkingLotDto> ParkingLots { get; set; }
        public ParkingLotsDataStore()
        {
            ParkingLots = new List<ParkingLotDto>()
            {
                new ParkingLotDto()
                {
                    Id = 1,
                    AreaId = 1,
                    Name = "Ionescu Parking",
                    Location = "placeholder for Long/Lat or street",
                    IsPaid = true,
                    IsStateOwned = false,
                    TotalParkingSpaces = 50,
                    HasSecurity = true
                },
                new ParkingLotDto()
                {
                    Id = 2,
                    AreaId = 1,
                    Name = "Popescu Parking",
                    Location = "placeholder for Long/Lat or street",
                    IsPaid = true,
                    IsStateOwned = false,
                    TotalParkingSpaces = 40,
                    HasSecurity = true
                },
                new ParkingLotDto()
                {
                    Id = 3,
                    AreaId = 1,
                    Name = "Iliescu Parking",
                    Location = "placeholder for Long/Lat or street",
                    IsPaid = true,
                    IsStateOwned = false,
                    TotalParkingSpaces = 70,
                    HasSecurity = true
                },
                new ParkingLotDto()
                {
                    Id = 4,
                    AreaId = 5,
                    Name = "CJ Ionescu Parking",
                    Location = "placeholder for Long/Lat or street",
                    IsPaid = true,
                    IsStateOwned = false,
                    TotalParkingSpaces = 30,
                    HasSecurity = true
                }
            };
        }
    }
}
