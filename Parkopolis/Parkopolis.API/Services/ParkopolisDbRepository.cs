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

        public void AddArea(Area area)
        {
            _context.Areas.Add(area);
            Save();
        }

        public void AddCity(City cityToAdd)
        {
            _context.Cities.Add(cityToAdd);
            Save();
        }

        public bool AreaExists(int id)
        {
            return _context.Areas.Any(a => a.Id == id);
        }

            public bool CityExists(int id)
        {
            return _context.Cities.Any(city => city.Id == id);
        }

        public IEnumerable<Area> GetAllAreas()
        {
           return _context.Areas.ToList();
            
        }

        public IEnumerable<Area> GetAllAreasForCity(int cityId)
        {
            return _context.Areas.Where(a => a.CityId == cityId);
        }

        public  IEnumerable<City> GetAllCities()
        {
           return  _context.Cities.ToList();
        }

        

        public City GetCityById(int id)
        {
            return _context.Cities.Where(c => c.Id == id).FirstOrDefault();
        }

        public void RemoveArea(Area area)
        {
            _context.Areas.Remove(area);
            Save();
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

        public Area GetAreaById(int id)
        {
            return _context.Areas.Where(a => a.Id == id).FirstOrDefault();
        }
    }
}
