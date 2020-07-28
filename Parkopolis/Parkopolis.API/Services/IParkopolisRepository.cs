using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Parkopolis.API.Services
{
    public interface IParkopolisRepository
    {
        void Save();
        public IEnumerable<City> GetAllCities();
        public bool CityExists(int id);
        public City GetCityById(int id);
        public void AddCity(City cityToAdd);
        public void RemoveCity(City city);
        public IEnumerable<Area> GetAllAreas();
        public IEnumerable<Area> GetAllAreasForCity(int cityId);
        public bool AreaExists(int id);
        public Area GetAreaById(int id);
        public void AddArea(Area area);
        public void RemoveArea(Area area);
    }
}
