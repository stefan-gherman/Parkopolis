using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parkopolis.API.Context;
using Parkopolis.API.Models;
using Parkopolis.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parkopolis.API.Services
{
    public class ParkopolisDbRepository : IParkopolisRepository
    {
        private readonly ParkopolisDbContext _context;
        private readonly IMapper _mapper;

       
        public ParkopolisDbRepository(ParkopolisDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public IEnumerable<ParkingLot> GetAllParkingLots()
        {
            return _context.ParkingLots.ToList();
        }

        public IEnumerable<ParkingLot> GetParkingLots(int areaId)
        {
            return _context.ParkingLots.Where(pl => pl.AreaId == areaId);
        }

        public bool ParkingLotExists(int id)
        {
            return _context.ParkingLots.Any(pl => pl.Id == id);
        }

        public ParkingLot GetParkingLotById(int id)
        {
            return _context.ParkingLots.Where(pl => pl.Id == id).FirstOrDefault();
        }

        public void AddParkingLot(ParkingLot parkingLot)
        {
            _context.ParkingLots.Add(parkingLot);
            Save();
        }

        public void RemoveParkingLot(ParkingLot parkingLot)
        {
            _context.ParkingLots.Remove(parkingLot);
            Save();
        }

        public void UpdateParkingLot(int id, ParkingLot parkingLot)
        {
            var updateParkingLot = _context.ParkingLots.SingleOrDefault(pl => pl.Id == id);

            CopyClass.CopyParkingLot(parkingLot, updateParkingLot);
            _context.ParkingLots.Update(updateParkingLot);
            Save();
        }

        public void PatchParkingLot(int id, ParkingLot parkingLot)
        {
            UpdateParkingLot(id, parkingLot);
            Save();
        }

        public IEnumerable<ParkingSpace> GetAllParkingSpaces()
        {
            return _context.ParkingSpaces.ToList();
        }

        public IEnumerable<ParkingSpace> GetParkingSpaces(int lotId)
        {
            return _context.ParkingSpaces.Where(ps => ps.ParkingLotId == lotId); ;
        }

        public bool ParkingSpaceExists(int id)
        {
            return _context.ParkingSpaces.Any(ps => ps.Id == id);
        }

        public ParkingSpace GetParkingSpaceById(int id)
        {
            return _context.ParkingSpaces.Where(ps => ps.Id == id).FirstOrDefault();
        }

        public void AddParkingSpace(ParkingSpace parkingSpace)
        {
            _context.ParkingSpaces.Add(parkingSpace);
            Save();
        }

        public void RemoveParkingSpace(ParkingSpace parkingSpace)
        {
            _context.ParkingSpaces.Remove(parkingSpace);
            Save();
        }

        public void UpdateParkingSpace(int id, ParkingSpace parkingSpace)
        {
            var updateParkingSpace = _context.ParkingSpaces.SingleOrDefault(pl => pl.Id == id);

            CopyClass.CopyParkingSpace(parkingSpace, updateParkingSpace);
            _context.ParkingSpaces.Update(updateParkingSpace);
            Save();
        }

        public void PatchParkingSpace(int id, ParkingSpace parkingSpace)
        {
            UpdateParkingSpace(id, parkingSpace);
        }

        public IEnumerable<ParkingLot> GetAllParkingLotsIncludingParkingSpaces()
        {
            return _context.ParkingLots.Include(pl=> pl.ParkingSpaces).ToList();
        }

        public ParkingLot GetParkingLotByIdIncludingParkingSpaces(int id)
        {
            return _context.ParkingLots.Include(pl => pl.ParkingSpaces).Where(pl => pl.Id == id).FirstOrDefault();
        }

        public IEnumerable<ParkingLot> GetParkingLotsIncludingParkingSpacesById(int areaId)
        {
            return _context.ParkingLots.Include(pl => pl.ParkingSpaces).Where(pl => pl.AreaId == areaId).ToList();
        }

        public void UpdateCity(int id, City city)
        {
            var cityUpdate = _context.Cities.SingleOrDefault(c => c.Id == id);

            CopyClass.CopyCity(city, cityUpdate);
            _context.Cities.Update(cityUpdate);
            Save();
        }

        public void UpdateArea(int id, Area area)
        {
            throw new NotImplementedException();
        }
    }
}
