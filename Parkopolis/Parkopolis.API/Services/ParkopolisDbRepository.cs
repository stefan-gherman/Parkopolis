using Microsoft.EntityFrameworkCore;
using Parkopolis.API.Context;
using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Services
{
    public class ParkopolisDbRepository : IParkopolisRepository
    {
        private readonly ParkopolisDbContext _context;

        public ParkopolisDbRepository(ParkopolisDbContext context)
        {
            _context = context;
        }

        public void AddCity(City cityToAdd)
        {
            _context.Cities.Add(cityToAdd);
        }

        public bool CityExists(int id)
        {
            return _context.Cities.Any(city => city.Id == id);
        }

        public  IEnumerable<City> GetAllCities()
        {
           return  _context.Cities.ToList();
        }

        public City GetCityById(int id)
        {
            return _context.Cities.Where(c => c.Id == id).FirstOrDefault();
        }

        public void RemoveCity(City city)
        {
            _context.Cities.Remove(city);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
