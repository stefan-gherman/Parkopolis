using Microsoft.AspNetCore.Mvc;
using Parkopolis.API.MockData;
using Parkopolis.API.Models;
using System.Linq;

namespace Parkopolis.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/areas/{areaId}/parkinglots/{parkingLotId}/parkingspaces")]
    public class ParckingSpacesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetParkingSpaces(int parkingLotId)
        {
            return Ok(ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.FindAll(p => p.ParkingLotId == parkingLotId));
        }

        [HttpGet("{parkingSpaceId}")]
        public IActionResult GetParkingSpace(int parkingLotId, int parkingSpaceId)
        {
            return Ok(ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.FindAll(p => p.Id == parkingLotId));
        }

        [HttpPost]
        public IActionResult CreateParkingSpace(int areaid, int parkingLotId, [FromBody] ParkingSpaceForCreationDto parkingSpace)
        {
            var maxParkingSpaceId = ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.Max(p => p.Id);

            var newParkingSpace = new ParkingSpaceDto()
            {
                Id = maxParkingSpaceId + 1,
                Name = parkingSpace.Name,
                Details = parkingSpace.Details,
                HasCarWash = parkingSpace.HasCarWash,
                IsCovered = parkingSpace.IsCovered,
                IsTaken = parkingSpace.IsTaken,
                ParkingLotId = parkingSpace.ParkingLotId,
                Price = parkingSpace.Price
            };

            ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.Add(newParkingSpace);

            return NoContent();
        }

        [HttpPut("{parkingSpaceId}")]
        public IActionResult UpdateParkingSpace(int areaid, int parkingLotId, int parkingSpaceId, [FromBody] ParkingSpaceForCreationDto parkingSpace)
        {
            var getParkingSpaceForUpdate = ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.FirstOrDefault(p => p.Id == parkingSpaceId);

            getParkingSpaceForUpdate.Name = parkingSpace.Name;
            getParkingSpaceForUpdate.Details = parkingSpace.Details;
            getParkingSpaceForUpdate.HasCarWash = parkingSpace.HasCarWash;
            getParkingSpaceForUpdate.IsCovered = parkingSpace.IsCovered;
            getParkingSpaceForUpdate.IsTaken = parkingSpace.IsTaken;
            getParkingSpaceForUpdate.ParkingLotId = parkingSpace.ParkingLotId;
            getParkingSpaceForUpdate.Price = parkingSpace.Price;

            return NoContent();
        }

        public IActionResult PartiallyUpdateParkingSpace(int areaId, int parkingLotId, int parkingSpaceId, [FromBody] ParkingSpaceForCreationDto parkingSpace)
        {
            //var parkingSpaceFromStore = ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.FirstOrDefault(p => p.Id == parkingSpaceId);

            //var parkingSpaceToPatch = new ParkingSpaceForUpdateDto()
            //{
            //    Name = parkingSpaceFromStore.Name,
            //    Details = parkingSpaceFromStore.Details,
            //    HasCarWash = parkingSpaceFromStore.HasCarWash,
            //    IsCovered = parkingSpaceFromStore.IsCovered,
            //    IsTaken = parkingSpaceFromStore.IsTaken,
            //    ParkingLotId = parkingSpaceFromStore.ParkingLotId,
            //    Price = parkingSpaceFromStore.Price
            //};

            //patchDoc.ApplyTo(parkingSpaceToPatch);

            //parkingSpaceToPatch.Name = parkingSpaceToPatch.Name,
            //    parkingSpaceToPatch.Details = parkingSpaceToPatch.Details,
            //    parkingSpaceToPatch.HasCarWash = parkingSpaceToPatch.HasCarWash,
            //    parkingSpaceToPatch.IsCovered = parkingSpaceToPatch.IsCovered,
            //    parkingSpaceToPatch.IsTaken = parkingSpaceToPatch.IsTaken,
            //    parkingSpaceToPatch.ParkingLotId = parkingSpaceToPatch.ParkingLotId,
            //    parkingSpaceToPatch.Price = parkingSpaceToPatch.Price

            return NoContent();
        }
    }
}