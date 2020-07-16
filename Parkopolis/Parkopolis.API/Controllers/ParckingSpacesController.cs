using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Parkopolis.API.Models;
using Parkopolis.API.MockData;
using System.Threading.Tasks;

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

            var newParkingSpace= new ParkingSpaceDto()
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

        public IActionResult UpdateParkingSpace(int areaid, int parkingLotId, int parkingSpaceId, [FromBody] ParkingSpaceForCreationDto parkingSpace)
        {
            return Ok();
        }
        public IActionResult PartiallyUpdateParkingSpace(int areaId, int parkingLotId, int parkingSpaceId, [FromBody] ParkingSpaceForCreationDto parkingSpace)
        {
            return Ok();

        }

    }
}
