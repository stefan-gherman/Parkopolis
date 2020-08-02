using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Profile
{
    public class ParkingSpaceProfile : AutoMapper.Profile
    {
        public ParkingSpaceProfile()
        {
            CreateMap<Models.ParkingSpace, Models.ParkingSpaceForUpdateDto>().ReverseMap();
        }
    }
}
