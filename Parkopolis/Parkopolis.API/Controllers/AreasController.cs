using Microsoft.AspNetCore.Mvc;
using Parkopolis.API.MockData;
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
        [HttpGet]
        public IActionResult GetAreas(int cityId)
        {
            if (!Validation.CityExists(cityId)) return NotFound();

            return Ok(AreasDataStore.CurrentAreas.Areas.FindAll( a => a.CityId == cityId));
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
