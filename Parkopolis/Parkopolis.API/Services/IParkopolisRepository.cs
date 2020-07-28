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
        //City
        public IEnumerable<City> GetAllCities();
        public bool CityExists(int id);
        public City GetCityById(int id);
        public void AddCity(City cityToAdd);
        public void RemoveCity(City city);

        //Areas
        public IEnumerable<Area> GetAllAreas();
        public IEnumerable<Area> GetAllAreasForCity(int cityId);
        public bool AreaExists(int id);
        public Area GetAreaById(int id);
        public void AddArea(Area area);
        public void RemoveArea(Area area);

        //Parking Lot
        public IEnumerable<ParkingLot> GetAllParkingLots();
        public IEnumerable<ParkingLot> GetParkingLots(int areaId);
        public bool ParkingLotExists(int id);
        public ParkingLot GetParkingLotById(int id);
        public void AddParkingLot(ParkingLot parkingLot);
        public void RemoveParkingLot(ParkingLot parkingLot);
        public void UpdateParkingLot(int id, ParkingLot parkingLot);
        public void PatchParkingLot(int id, ParkingLot parkingLot);


        //Parking Space
        public IEnumerable<ParkingSpace> GetAllParkingSpaces();
        public IEnumerable<ParkingSpace> GetParkingSpaces(int lotId);
        public bool ParkingSpaceExists(int id);
        public ParkingSpace GetParkingSpaceById(int id);
        public void AddParkingSpace(ParkingSpace parkingSpace);
        public void RemoveParkingSpace(ParkingSpace parkingSpace);
        public void UpdateParkingSpace(int id, ParkingSpace parkingSpace);
        public void PatchParkingSpace(int id, ParkingSpace parkingSpace);
    }
}
