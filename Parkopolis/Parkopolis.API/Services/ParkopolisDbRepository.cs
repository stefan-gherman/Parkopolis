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
            var areaUpdate = _context.Areas.SingleOrDefault(a => a.Id == id);
            CopyClass.CopyArea(area, areaUpdate);
            _context.Areas.Update(areaUpdate);
            Save();
        }

        public IEnumerable<ParkingLot> GetAllParkingLotsForUser(string userId)
        {
            return _context.ParkingLots.Where(pl => pl.ApplicationUserId == userId);
        }

        public bool UserExists(string id)
        {
            return _context.Users.Any(u => u.Id.Equals(id));
        }

        public IEnumerable<City> GetCitiesWithAreaCount()
        {
            return _context.Cities.Include(c => c.Areas).ToList();
        }

        public City GetCityWithAreaCount(int cityId)
        {
            return _context.Cities.Include(c => c.Areas).Where(c => c.Id == cityId).FirstOrDefault();
        }

        public IEnumerable<Area> GetAreasWithParkingLotCount(int cityId)
        {
            return _context.Areas.Include(a => a.ParkingLots).ToList();
        }

        public Area GetAreaWithParkingLotCount(int cityId, int areaId)
        {
            return _context.Areas.Include(a => a.ParkingLots).Where(a => a.Id == areaId && a.CityId == cityId).FirstOrDefault();
        }

        public IEnumerable<ParkingLot> GetAllParkingLotsForUserIncludingParkingSpots(string userId)
        {
            return _context.ParkingLots.Include(pl => pl.ParkingSpaces).Where(pl => pl.ApplicationUserId == userId);
        }

        public void DeleteUser(string userId)
        {
            _context.ParkingLots.RemoveRange(_context.ParkingLots.Where(pl => pl.ApplicationUserId == userId));
            Save();
            var userToRemove = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            _context.Users.Remove(userToRemove);
            Save();
        }

        public bool AreaIsInCity(int areaId, int cityId)
        {
            var area = _context.Areas.Where(a => a.Id == areaId).FirstOrDefault();
            if (area.CityId == cityId) return true;
            return false;
        }

        public bool ParkingLotIsInArea(int areaId, int lotId)
        {
            var parkingLot = _context.ParkingLots.Where(pl => pl.Id == lotId).FirstOrDefault();
            if (parkingLot.AreaId == areaId) return true;
            return false;
        }

        public bool ParkingSpaceIsInParkingLot(int parkingSpaceId, int lotId)
        {
            var parkingSpace = _context.ParkingSpaces.Where(ps => ps.Id == parkingSpaceId).FirstOrDefault();
            if (parkingSpace.ParkingLotId == lotId) return true;
            return false;
        }

        public bool UserOwnsParkingLot(int parkingLotId, string userId)
        {
            var parkingLot = _context.ParkingLots.Where(pl => pl.Id == parkingLotId).FirstOrDefault();
            if (parkingLot.ApplicationUserId.Equals(userId))
            {
                return true;
            }
            return false;
        }
    }
}
