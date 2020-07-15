using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.MockData
{
    public class CitiesDataStore
    { 
        public static CitiesDataStore CurrentCities { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }
        
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Bucharest"
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Cluj-Napoca"
                }
            };
        }
    }
}
