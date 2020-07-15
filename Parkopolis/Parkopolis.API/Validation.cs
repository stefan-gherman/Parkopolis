using Parkopolis.API.MockData;
using Parkopolis.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API
{
    public class Validation
    {
        public static bool CityExists(int cityId)
        {
            var city = CitiesDataStore.CurrentCities.Cities
                .FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return false;
            }
            return true;
        }

        public static bool AreaExists(int cityId, int areaId)
        {
            var areas = AreasDataStore.CurrentAreas.Areas.FindAll(a => a.Id == areaId && cityId == a.CityId);
            if (areas.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static bool ParkingLotExists(int areaId)
        {
            var parkingLots = ParkingLotsDataStore.CurrentParkingLots.ParkingLots.FindAll(p => p.AreaId == areaId);
            if (parkingLots.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static bool SingleParkingLotExists(int parkingLotId)
        {
            var parkingLot = ParkingLotsDataStore.CurrentParkingLots.ParkingLots.FirstOrDefault(p => p.Id == parkingLotId);
            if (parkingLot==null)
            {
                return false;
            }
            return true;
        }
    }
}
