using Microsoft.AspNetCore.Mvc;
using Parkopolis.API.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/areas/{areaId}/parkinglots")]
    public class ParkingLotsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetParkingLots(int cityId, int areaId)
        {
            if (!Validation.CityExists(cityId) || !Validation.AreaExists(cityId, areaId)) return NotFound();

            if (!Validation.ParkingLotExists(areaId)) return NotFound();

            return Ok(ParkingLotsDataStore.CurrentParkingLots.ParkingLots.FindAll(p => p.AreaId == areaId));
        }

        [HttpGet("{parkingLotId}")]
        public IActionResult GetParkingLot(int cityId, int areaId, int parkingLotId)
        {
            if (!Validation.CityExists(cityId) || !Validation.AreaExists(cityId, areaId) || !Validation.SingleParkingLotExists(parkingLotId)) return NotFound();

            if (!Validation.ParkingLotExists(areaId)) return NotFound();

            return Ok(ParkingLotsDataStore.CurrentParkingLots.ParkingLots.FirstOrDefault(p => p.Id == parkingLotId));
        }
    }

}
