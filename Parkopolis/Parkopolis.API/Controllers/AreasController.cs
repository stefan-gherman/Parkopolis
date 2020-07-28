using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parkopolis.API.MockData;
using Parkopolis.API.Models;
using Parkopolis.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/areas")]
    public class AreasController : ControllerBase
    {
        IMapper _mapper;
        IParkopolisRepository _repo;

        public AreasController (IMapper mapper, IParkopolisRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAreas(int cityId)
        {
            if(!_repo.CityExists(cityId))
            {
                return NotFound("City Not Found");
            }

            return Ok(_mapper.Map<IEnumerable<AreaForDisplayDto>>(_repo.GetAllAreasForCity(cityId)));
        }

        [HttpGet("{areaId}")]
        public IActionResult GetArea(int cityId, int areaId)
        {
            if (!Validation.CityExists(cityId)) return NotFound();

            if (!Validation.AreaExists(cityId, areaId)) return NotFound();

            var result = AreasDataStore.CurrentAreas.Areas.FindAll(a => a.Id == areaId && cityId == a.CityId);

            return Ok(result);
        }
    }
}
