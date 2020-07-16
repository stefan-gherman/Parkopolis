using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Parkopolis.API.MockData;
using Parkopolis.API.Models;
using System.Linq;

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

        [HttpPost]
        public void CreateParkingLot(int areaid, [FromBody] ParkingLotForCreationDto parkingLot)
        {
            var maxParkingLotId = ParkingLotsDataStore.CurrentParkingLots.ParkingLots.Max(p => p.Id);

            var newParkingLot = new ParkingLotDto()
            {
                Id = maxParkingLotId + 1,
                Name = parkingLot.Name,
                AreaId = parkingLot.AreaId,
                HasSecurity = parkingLot.HasSecurity,
                IsPaid = parkingLot.IsPaid,
                IsStateOwned = parkingLot.IsStateOwned,
                Location = parkingLot.Location,
                TotalParkingSpaces = parkingLot.TotalParkingSpaces
            };

            ParkingLotsDataStore.CurrentParkingLots.ParkingLots.Add(newParkingLot);
        }

        [HttpPut("{parkingLotId}")]
        public IActionResult UpdateParkingLot(int areaId, int parkingLotId, [FromBody] ParkingLotForCreationDto parkingLot)
        {
            var getParkingLotForUpdate = ParkingLotsDataStore.CurrentParkingLots.ParkingLots.FirstOrDefault(p => p.Id == parkingLotId);

            getParkingLotForUpdate.Name = parkingLot.Name;
            getParkingLotForUpdate.AreaId = parkingLot.AreaId;
            getParkingLotForUpdate.HasSecurity = parkingLot.HasSecurity;
            getParkingLotForUpdate.IsPaid = parkingLot.IsPaid;
            getParkingLotForUpdate.IsStateOwned = parkingLot.IsStateOwned;
            getParkingLotForUpdate.Location = parkingLot.Location;
            getParkingLotForUpdate.TotalParkingSpaces = parkingLot.TotalParkingSpaces;

            return NoContent();
        }

        [HttpPatch("{parkingLotId}")]
        public IActionResult PartiallyUpdateParkingLot(int areaId, int parkingLotId, [FromBody] JsonPatchDocument<ParkingLotForUpdateDto> patchDoc)
        {
            var parkingLotFromStore = ParkingLotsDataStore.CurrentParkingLots.ParkingLots.FirstOrDefault(p => p.Id == parkingLotId);

            var parkingLotToPatch = new ParkingLotForUpdateDto()
            {
                Name = parkingLotFromStore.Name,
                AreaId = parkingLotFromStore.AreaId,
                HasSecurity = parkingLotFromStore.HasSecurity,
                IsPaid = parkingLotFromStore.IsPaid,
                IsStateOwned = parkingLotFromStore.IsStateOwned,
                Location = parkingLotFromStore.Location,
                TotalParkingSpaces = parkingLotFromStore.TotalParkingSpaces
            };

            patchDoc.ApplyTo(parkingLotToPatch);

            parkingLotFromStore.Name = parkingLotToPatch.Name;
            parkingLotFromStore.AreaId = parkingLotToPatch.AreaId;
            parkingLotFromStore.HasSecurity = parkingLotToPatch.HasSecurity;
            parkingLotFromStore.IsPaid = parkingLotToPatch.IsPaid;
            parkingLotFromStore.IsStateOwned = parkingLotToPatch.IsStateOwned;
            parkingLotFromStore.Location = parkingLotToPatch.Location;
            parkingLotFromStore.TotalParkingSpaces = parkingLotToPatch.TotalParkingSpaces;

            return NoContent();
        }

        [HttpDelete("{parkingLotId}")]
        public IActionResult DeleteParkingLot(int areaId, int parkingLotId)
        {
            var parkingLotToDelete = ParkingLotsDataStore.CurrentParkingLots.ParkingLots.FirstOrDefault(p => p.Id == parkingLotId);

            ParkingLotsDataStore.CurrentParkingLots.ParkingLots.Remove(parkingLotToDelete);

            return NoContent();
        }
    }
}