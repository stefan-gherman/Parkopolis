using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.MockData
{
    public class AreasDataStore
    {
        public static AreasDataStore CurrentAreas { get; } = new AreasDataStore();
        public List<AreaDto> Areas { get; set; }
        public AreasDataStore()
        {
            Areas = new List<AreaDto>()
            {
                new AreaDto()
                {
                    Id = 1,
                    CityId = 1,
                    Name = "Union Square"
                },
                new AreaDto()
                {
                    Id = 2,
                    CityId = 1,
                    Name = "University Square"
                },
                new AreaDto()
                {
                    Id = 3,
                    CityId = 1,
                    Name = "Obor Square"
                },
                new AreaDto()
                {
                    Id = 4,
                    CityId = 2,
                    Name = "Center"
                },
                new AreaDto()
                {
                    Id = 5,
                    CityId = 2,
                    Name = "Gruia"
                }
            };
        }

    }
}
