using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.MockData
{
    public class ParkingSpacesDataStore
    {
        public static ParkingSpacesDataStore CurrentParkingSpaces { get; } = new ParkingSpacesDataStore();
        public List<ParkingSpaceDto> ParkingSpaces { get; set; }
        public ParkingSpacesDataStore()
        {
            ParkingSpaces = new List<ParkingSpaceDto>()
            {
                new ParkingSpaceDto()
                {
                    Id = 1,
                    ParkingLotId = 1,
                    Name = "I1",
                    IsTaken = false,
                    HasCarWash = true,
                    IsCovered = true,
                    Price = 7.5M,
                    Details = "Close to exit."
                },
                new ParkingSpaceDto()
                {
                    Id = 2,
                    ParkingLotId = 1,
                    Name = "I2",
                    IsTaken = false,
                    HasCarWash = true,
                    IsCovered = true,
                    Price = 7.5M,
                    Details = "Close to exit."
                },
                new ParkingSpaceDto()
                {
                    Id = 3,
                    ParkingLotId = 1,
                    Name = "I3",
                    IsTaken = false,
                    HasCarWash = true,
                    IsCovered = true,
                    Price = 7.5M,
                    Details = "Close to exit."
                },
                new ParkingSpaceDto()
                {
                    Id = 4,
                    ParkingLotId = 2,
                    Name = "P1",
                    IsTaken = false,
                    HasCarWash = true,
                    IsCovered = true,
                    Price = 7.5M,
                    Details = "Close to exit."
                },
                new ParkingSpaceDto()
                {
                    Id = 5,
                    ParkingLotId = 2,
                    Name = "P2",
                    IsTaken = false,
                    HasCarWash = true,
                    IsCovered = true,
                    Price = 7.5M,
                    Details = "Close to exit."
                },
                new ParkingSpaceDto()
                {
                    Id = 6,
                    ParkingLotId = 2,
                    Name = "P#",
                    IsTaken = false,
                    HasCarWash = true,
                    IsCovered = true,
                    Price = 7.5M,
                    Details = "Close to exit."
                },
                new ParkingSpaceDto()
                {
                    Id = 7,
                    ParkingLotId = 3,
                    Name = "ILI1",
                    IsTaken = false,
                    HasCarWash = true,
                    IsCovered = true,
                    Price = 7.5M,
                    Details = "Close to exit."
                },
                new ParkingSpaceDto()
                {
                    Id = 8,
                    ParkingLotId = 3,
                    Name = "ILI1",
                    IsTaken = false,
                    HasCarWash = true,
                    IsCovered = true,
                    Price = 7.5M,
                    Details = "Close to exit."
                },
                new ParkingSpaceDto()
                {
                    Id = 9,
                    ParkingLotId = 3,
                    Name = "ILI1",
                    IsTaken = false,
                    HasCarWash = true,
                    IsCovered = true,
                    Price = 7.5M,
                    Details = "Close to exit."
                }
            };
        }

    }
}
