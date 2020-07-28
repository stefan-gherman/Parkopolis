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
        public IActionResult GetParkingSpaces(int parkingLotId)
        {
            return Ok(_repo.GetParkingSpaces(parkingLotId));
            //return Ok(ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.FindAll(p => p.ParkingLotId == parkingLotId));
        }

        [HttpGet("{parkingSpaceId}")]
        public IActionResult GetParkingSpace(int parkingLotId, int parkingSpaceId)
        {
            return Ok(_repo.GetParkingSpaceById(parkingSpaceId));
            //return Ok(ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.FindAll(p => p.Id == parkingLotId));
        }

        [HttpPost]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult CreateParkingSpace(int areaId, int parkingLotId, [FromBody] ParkingSpace parkingSpace)
        {
            //var maxParkingSpaceId = ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.Max(p => p.Id);

            //var newParkingSpace = new ParkingSpaceDto()
            //{
            //    Id = maxParkingSpaceId + 1,
            //    Name = parkingSpace.Name,
            //    Details = parkingSpace.Details,
            //    HasCarWash = parkingSpace.HasCarWash,
            //    IsCovered = parkingSpace.IsCovered,
            //    IsTaken = parkingSpace.IsTaken,
            //    ParkingLotId = parkingSpace.ParkingLotId,
            //    Price = parkingSpace.Price
            //};

            //ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.Add(newParkingSpace);

            _repo.AddParkingSpace(parkingSpace);

            return NoContent();
        }

        [HttpPut("{parkingSpaceId}")]
        public IActionResult UpdateParkingSpace(int areaId, int parkingLotId, int parkingSpaceId, [FromBody] ParkingSpaceForCreationDto parkingSpace)
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

        [HttpPatch("{parkingSpaceId}")]
        public IActionResult PartiallyUpdateParkingSpace(int areaId, int parkingLotId, int parkingSpaceId,
            [FromBody] JsonPatchDocument<ParkingSpaceForUpdateDto> patchDoc)
        {
            var parkingSpaceFromStore = ParkingSpacesDataStore.CurrentParkingSpaces.ParkingSpaces.FirstOrDefault(p => p.Id == parkingSpaceId);

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

            parkingSpaceFromStore.Name = parkingSpaceToPatch.Name;
            parkingSpaceFromStore.Details = parkingSpaceToPatch.Details;
            parkingSpaceFromStore.HasCarWash = parkingSpaceToPatch.HasCarWash;
            parkingSpaceFromStore.IsCovered = parkingSpaceToPatch.IsCovered;
            parkingSpaceFromStore.IsTaken = parkingSpaceToPatch.IsTaken;
            parkingSpaceFromStore.ParkingLotId = parkingSpaceToPatch.ParkingLotId;
            parkingSpaceFromStore.Price = parkingSpaceToPatch.Price;

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