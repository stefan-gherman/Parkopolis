using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Profile
{
    public class CityProfile : AutoMapper.Profile
    {
        public CityProfile()
        {
            CreateMap<Models.City, Models.CityDto>().ForMember(dest => dest.AreaCount,
                opt => opt.MapFrom(source => source.Areas.Count));
            CreateMap<Models.City, Models.City>();
        }
    }
}
