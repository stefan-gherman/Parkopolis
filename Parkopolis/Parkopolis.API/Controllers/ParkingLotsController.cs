﻿using AutoMapper;
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
    [Route("api/cities/{cityId}/areas/{areaId}/parkinglots")]
    public class ParkingLotsController : ControllerBase
    {
        IMapper _mapper;
        IParkopolisRepository _repo;

        public ParkingLotsController(IMapper mapper, IParkopolisRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public IActionResult GetParkingLots(int cityId, int areaId)
        {
            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

           

            return Ok(_repo.GetParkingLots(areaId));
        }

        [HttpGet("{parkingLotId}")]
        public IActionResult GetParkingLot(int cityId, int areaId, int parkingLotId)
        {
            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

            if (!_repo.ParkingLotExists(parkingLotId)) return NotFound("Lot not found");


            return Ok(_repo.GetParkingLots(areaId));
        }

        [HttpPost]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult CreateParkingLot(int cityId, int areaId, [FromBody] ParkingLot parkingLot)
        {
            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

            //parkingLot.AreaId = areaId;

            _repo.AddParkingLot(parkingLot);
            return NoContent();
        }

        [HttpPut("{parkingLotId}")]
        public IActionResult UpdateParkingLot(int cityId, int areaId, int parkingLotId, [FromBody] ParkingLot parkingLot)
        {
            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

            if (!_repo.ParkingLotExists(parkingLotId)) return NotFound("Lot not found");

            if (!_repo.AreaExists(parkingLot.AreaId)) return NotFound("AreaId From Query is Invalid");

            parkingLot.Id = parkingLotId;
            _repo.UpdateParkingLot(parkingLotId, parkingLot);

            return NoContent();
        }

        [HttpPatch("{parkingLotId}")]
        public IActionResult PartiallyUpdateParkingLot(int cityId, int areaId, int parkingLotId, [FromBody] JsonPatchDocument<ParkingLotForUpdateDto> patchDoc)
        {
            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

            if (!_repo.ParkingLotExists(parkingLotId)) return NotFound("Lot not found");

            
            var parkingLotFromStore = _repo.GetParkingLotById(parkingLotId);

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

            if (!_repo.AreaExists(parkingLotToPatch.AreaId)) return NotFound("AreaId From Query is Invalid");

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
        public IActionResult DeleteParkingLot(int cityId, int areaId, int parkingLotId)
        {
            if (!_repo.CityExists(cityId)) return NotFound("City not found");

            if (!_repo.AreaExists(areaId)) return NotFound("Area not found");

            if (!_repo.ParkingLotExists(parkingLotId)) return NotFound("Lot not found");

            _repo.RemoveParkingLot(_repo.GetParkingLotById(parkingLotId));

            return NoContent();
        }
    }
}