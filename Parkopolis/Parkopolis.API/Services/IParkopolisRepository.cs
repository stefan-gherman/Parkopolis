using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
