using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Profile
{
    public class ParkingLotProfile : AutoMapper.Profile
    {
        public ParkingLotProfile()
        {
            CreateMap<Models.ParkingLot, Models.ParkingLotDto>();
        }
    }
}
