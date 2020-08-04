using AutoMapper;
using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Profile
{
    public class AreaProfile : AutoMapper.Profile
    {
        public AreaProfile()
        {
            CreateMap<Models.Area, Models.AreaForDisplayDto>().ForMember(dst => dst.ParkingLotCount,
                opt => opt.MapFrom(src => src.ParkingLots.Count));
            CreateMap<Models.Area, Models.AreaForDisplayNoCountDto>();
        }
    }

}
