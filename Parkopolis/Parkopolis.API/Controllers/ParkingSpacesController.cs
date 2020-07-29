using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Parkopolis.API.MockData;
using Parkopolis.API.Models;
using Parkopolis.API.Services;
using System.Linq;

namespace Parkopolis.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/areas/{areaId}/parkinglots/{parkingLotId}/parkingspaces")]
    public class ParkingSpacesController : ControllerBase
    {
        IParkopolisRepository _repo;
        IMapper _mapper;
        public ParkingSpacesController(IMapper mapper, IParkopolisRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet] 
        public IActionResult GetParkingSpaces(int cityId, int areaId, int parkingLotId)
        {
            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

            if (!_repo.ParkingLotExists(parkingLotId)) return NotFound("Parking Lot not found!");
            return Ok(_repo.GetParkingSpaces(parkingLotId));
            
        }

        [HttpGet("{parkingSpaceId}")]
        public IActionResult GetParkingSpace(int cityId, int areaId, int parkingLotId, int parkingSpaceId)
        {
            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

            if (!_repo.ParkingLotExists(parkingLotId)) return NotFound("Parking Lot not found!");

            if (!_repo.ParkingSpaceExists(parkingSpaceId)) return NotFound("Parking space not found");
            
            return Ok(_repo.GetParkingSpaceById(parkingSpaceId));
           
        }

        [HttpPost]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult CreateParkingSpace(int cityId, int areaId, int parkingLotId, [FromBody] ParkingSpace parkingSpace)
        {
            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

            if (!_repo.ParkingLotExists(parkingLotId)) return NotFound("Parking Lot not found!");

            if (!_repo.ParkingLotExists(parkingSpace.ParkingLotId)) return NotFound("Invalid Parking Lot From Query");
            _repo.AddParkingSpace(parkingSpace);

            return NoContent();
        }

        [HttpPut("{parkingSpaceId}")]
        public IActionResult UpdateParkingSpace(int cityId, int areaId, int parkingLotId, int parkingSpaceId, [FromBody] ParkingSpace parkingSpace)
        {

            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

            if (!_repo.ParkingLotExists(parkingLotId)) return NotFound("Parking Lot not found!");

            if (!_repo.ParkingSpaceExists(parkingSpaceId)) return NotFound("Parking space not found");

            if (!_repo.ParkingLotExists(parkingSpace.ParkingLotId)) return NotFound("Invalid Parking Lot From Query");

            parkingSpace.Id = parkingSpaceId;

            _repo.UpdateParkingSpace(parkingSpaceId, parkingSpace);

            return NoContent();
        }

        [HttpPatch("{parkingSpaceId}")]
        public IActionResult PartiallyUpdateParkingSpace(int cityId, int areaId, int parkingLotId, int parkingSpaceId,
            [FromBody] JsonPatchDocument<ParkingSpaceForUpdateDto> patchDoc)
        {
            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

            if (!_repo.ParkingLotExists(parkingLotId)) return NotFound("Parking Lot not found!");

            if (!_repo.ParkingSpaceExists(parkingSpaceId)) return NotFound("Parking space not found");

            
            var parkingSpaceFromStore = _repo.GetParkingSpaceById(parkingSpaceId);

            var parkingSpaceToPatch = new ParkingSpaceForUpdateDto()
            {
                Name = parkingSpaceFromStore.Name,
                Details = parkingSpaceFromStore.Details,
                HasCarWash = parkingSpaceFromStore.HasCarWash,
                IsCovered = parkingSpaceFromStore.IsCovered,
                IsTaken = parkingSpaceFromStore.IsTaken,
                ParkingLotId = parkingSpaceFromStore.ParkingLotId,
                Price = parkingSpaceFromStore.Price
            };



            patchDoc.ApplyTo(parkingSpaceToPatch);

            if (!_repo.ParkingLotExists(parkingSpaceToPatch.ParkingLotId)) return NotFound("Invalid Parking Lot From Query");
           
            parkingSpaceFromStore.Name = parkingSpaceToPatch.Name;
            parkingSpaceFromStore.Details = parkingSpaceToPatch.Details;
            parkingSpaceFromStore.HasCarWash = parkingSpaceToPatch.HasCarWash;
            parkingSpaceFromStore.IsCovered = parkingSpaceToPatch.IsCovered;
            parkingSpaceFromStore.IsTaken = parkingSpaceToPatch.IsTaken;
            parkingSpaceFromStore.ParkingLotId = parkingSpaceToPatch.ParkingLotId;
            parkingSpaceFromStore.Price = parkingSpaceToPatch.Price;

            _repo.PatchParkingSpace(parkingSpaceId, parkingSpaceFromStore);

            return NoContent();
        }

        [HttpDelete("{parkingSpaceId}")]
        public IActionResult DeleteParkingLot(int areaId, int parkingSpaceId)
        {


            _repo.RemoveParkingSpace(_repo.GetParkingSpaceById(parkingSpaceId));

            return NoContent();
        }
    }
}