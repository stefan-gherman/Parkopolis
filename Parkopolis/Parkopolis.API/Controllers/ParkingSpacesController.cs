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
            string validationResult = ValidateCityAreaLot(cityId, areaId, parkingLotId);

            if (!validationResult.Equals("Ok")) return NotFound(validationResult);
            return Ok(_repo.GetParkingSpaces(parkingLotId));

        }

        [HttpGet("{parkingSpaceId}")]
        public IActionResult GetParkingSpace(int cityId, int areaId, int parkingLotId, int parkingSpaceId)
        {
            string validationResult = ValidateCityAreaLotSpace(cityId, areaId, parkingLotId, parkingSpaceId);
            if (!validationResult.Equals("Ok")) return NotFound(validationResult);
            return Ok(_repo.GetParkingSpaceById(parkingSpaceId));

        }

        [HttpGet("/users/{userId}/parkingspacesviewer/{parkingLotId}/getparkingspace/{parkingSpaceId}")]
        public IActionResult GetParkingSpaceFromUser (string userId, int parkingLotId, int parkingSpaceId)
        {

            string validationResult = ValidateUserLotSpace(userId, parkingLotId, parkingSpaceId);
            if (!validationResult.Equals("Ok")) return NotFound(validationResult);
            return Ok(_repo.GetParkingSpaceById(parkingSpaceId));
        }

        [HttpPost]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult CreateParkingSpace(int cityId, int areaId, int parkingLotId, [FromBody] ParkingSpace parkingSpace)
        {

            string validationResult = ValidateCityAreaLot(cityId, areaId, parkingLotId);

            if (!validationResult.Equals("Ok")) return NotFound(validationResult);
            if (!_repo.ParkingLotExists(parkingSpace.ParkingLotId)) return NotFound("Invalid Parking Lot From Query");
            if (!ModelState.IsValid)
            {
                return BadRequest("Your query is badly formatted");
            }
            _repo.AddParkingSpace(parkingSpace);

            return NoContent();
        }

        [HttpPost("/users/{userId}/lotmanager/{parkingLotId}/addparkingspace")]
        public IActionResult PostParkingLotFromUser(string userId, int parkingLotId, [FromBody] ParkingSpace parkingSpace)
        {
            if (!_repo.UserExists(userId)) return NotFound("User Not Found");
            if (!_repo.ParkingLotExists(parkingLotId)) return NotFound("Parking Lot not found!");

            if (!ModelState.IsValid)
            {
                return BadRequest("Your query is badly formatted");
            }
            parkingSpace.ParkingLotId = parkingLotId;
            _repo.AddParkingSpace(parkingSpace);
            return NoContent();
        }

        [HttpPut("{parkingSpaceId}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult UpdateParkingSpace(int cityId, int areaId, int parkingLotId, int parkingSpaceId, [FromBody] ParkingSpace parkingSpace)
        {

            string validationResult = ValidateCityAreaLotSpace(cityId, areaId, parkingLotId, parkingSpaceId);
            if (!validationResult.Equals("Ok")) return NotFound(validationResult);

            if (!_repo.ParkingLotExists(parkingSpace.ParkingLotId)) return NotFound("Invalid Parking Lot From Query");

            parkingSpace.Id = parkingSpaceId;

            if (!ModelState.IsValid)
            {
                return BadRequest("Your query is badly formatted");
            }
            _repo.UpdateParkingSpace(parkingSpaceId, parkingSpace);
            
            return NoContent();
        }

        [HttpPut("/users/{userId}/lotmanager/{parkingLotId}/editparkingspace/{parkingSpaceId}")]
        public IActionResult UpdateParkingSpaceFromUser (string userId, int parkingLotId, int parkingSpaceId,  ParkingSpace parkingSpace )
        {

            string validationResult = ValidateUserLotSpace(userId, parkingLotId, parkingSpaceId);
            if (!validationResult.Equals("Ok")) return NotFound(validationResult);

            if (!_repo.ParkingLotExists(parkingSpace.ParkingLotId)) return NotFound("Invalid Parking Lot From Query");
            parkingSpace.Id = parkingSpaceId;

            if (!ModelState.IsValid)
            {
                return BadRequest("Your query is badly formatted");
            }
            _repo.UpdateParkingSpace(parkingSpaceId, parkingSpace);

            return NoContent();
        }

        [HttpPatch("{parkingSpaceId}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult PartiallyUpdateParkingSpace(int cityId, int areaId, int parkingLotId, int parkingSpaceId,
            [FromBody] JsonPatchDocument<ParkingSpaceForUpdateDto> patchDoc)
        {

            string validationResult = ValidateCityAreaLotSpace(cityId, areaId, parkingLotId, parkingSpaceId);
            if (!validationResult.Equals("Ok")) return NotFound(validationResult);

            var parkingSpaceFromStore = _repo.GetParkingSpaceById(parkingSpaceId);

            var parkingSpaceToPatch = _mapper.Map<ParkingSpaceForUpdateDto>(parkingSpaceFromStore);



            patchDoc.ApplyTo(parkingSpaceToPatch);

            if (!_repo.ParkingLotExists(parkingSpaceToPatch.ParkingLotId)) return NotFound("Invalid Parking Lot From Query");

            _mapper.Map(parkingSpaceToPatch, parkingSpaceFromStore);

            if (!ModelState.IsValid)
            {
                return BadRequest("Your query is badly formatted");
            }
            _repo.PatchParkingSpace(parkingSpaceId, parkingSpaceFromStore);

            return NoContent();
        }

        [HttpPatch("/users/{userId}/lotmanager/{parkingLotId}/editparkingspace/{parkingSpaceId}")]
        public IActionResult PartialyUpdateParkingSpaceFromUser (string userId, int parkingLotId, int parkingSpaceId, [FromBody] JsonPatchDocument<ParkingSpaceForUpdateDto> patchDoc)
        {
            string validationResult = ValidateUserLotSpace(userId, parkingLotId, parkingSpaceId);
            if (!validationResult.Equals("Ok")) return NotFound(validationResult);

            var parkingSpaceFromStore = _repo.GetParkingSpaceById(parkingSpaceId);

            var parkingSpaceToPatch = _mapper.Map<ParkingSpaceForUpdateDto>(parkingSpaceFromStore);



            patchDoc.ApplyTo(parkingSpaceToPatch);

            if (!_repo.ParkingLotExists(parkingSpaceToPatch.ParkingLotId)) return NotFound("Invalid Parking Lot From Query");

            _mapper.Map(parkingSpaceToPatch, parkingSpaceFromStore);

            if (!ModelState.IsValid)
            {
                return BadRequest("Your query is badly formatted");
            }
            _repo.PatchParkingSpace(parkingSpaceId, parkingSpaceFromStore);

            return NoContent();
        }

        [HttpDelete("{parkingSpaceId}")]
        public IActionResult DeleteParkingSpace(int cityId, int areaId,int parkingLotId, int parkingSpaceId)
        {

            string validationResult = ValidateCityAreaLotSpace(cityId, areaId, parkingLotId, parkingSpaceId);
            if (!validationResult.Equals("Ok")) return NotFound(validationResult);
            _repo.RemoveParkingSpace(_repo.GetParkingSpaceById(parkingSpaceId));

            return NoContent();
        }

       [HttpDelete("/users/{userId}/lotmanager/{parkingLotId}/deleteparkingspace/{parkingSpaceId}")]

       public IActionResult DeleteParkingSpaceFromUser(string userId, int parkingLotId, int parkingSpaceId)
        {
            string validationResult = ValidateUserLotSpace(userId, parkingLotId, parkingSpaceId);
            if (!validationResult.Equals("Ok")) return NotFound(validationResult);

            _repo.RemoveParkingSpace(_repo.GetParkingSpaceById(parkingSpaceId));

            return NoContent();
        }

        public string ValidateCityAreaLot(int cityId, int areaId, int parkingLotId)
        {
            if (!_repo.CityExists(cityId)) return "City not found";
            if (!_repo.AreaExists(areaId)) return"Area not found";
            if (!_repo.AreaIsInCity(areaId, cityId)) return "Area not found";
            if (!_repo.ParkingLotExists(parkingLotId)) return "Parking Lot not found!";
            if (!_repo.ParkingLotIsInArea(areaId, parkingLotId)) return"Parking Lot not found!";

            return "Ok";
        }

        public string ValidateCityAreaLotSpace(int cityId, int areaId, int parkingLotId, int parkingSpaceId)
        {
            if(!_repo.CityExists(cityId)) return"City not found";

            if (!_repo.AreaExists(areaId)) return "Area not found";
            if (!_repo.AreaIsInCity(areaId, cityId)) return "Area not found";
            if (!_repo.ParkingLotExists(parkingLotId)) return"Parking Lot not found!";
            if (!_repo.ParkingLotIsInArea(areaId, parkingLotId)) return "Parking Lot not found!";
            if (!_repo.ParkingSpaceExists(parkingSpaceId)) return"Parking space not found";
            if (!_repo.ParkingSpaceIsInParkingLot(parkingSpaceId, parkingLotId)) return "Parking Space not Found";
            return "OK";
        }

        public string ValidateUserLotSpace(string userId, int parkingLotId, int parkingSpaceId)
        {
            if (!_repo.UserExists(userId)) return "User Not Found";
            if (!_repo.ParkingLotExists(parkingLotId)) return "Parking Lot not found!";
            if (!_repo.UserOwnsParkingLot(parkingLotId,userId)) return "Parking lot not found!";
            if (!_repo.ParkingSpaceExists(parkingSpaceId)) return "Parking space not found";
            if (!_repo.ParkingSpaceIsInParkingLot(parkingSpaceId, parkingLotId)) return "Parking space not found";
            return "Ok";
        }
    }
}