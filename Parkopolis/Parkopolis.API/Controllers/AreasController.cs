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
            var city = CitiesDataStore.CurrentCities.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(AreasDataStore.CurrentAreas.Areas.FindAll( a => a.CityId == cityId));
        }

        [HttpGet("{id}")]
        public IActionResult Getarea(int cityId, int id)
        {
            // find city
            var cityToReturn = CitiesDataStore.CurrentCities.Cities
                .FirstOrDefault(c => c.Id == cityId);

            if (cityToReturn == null)
            {
                return NotFound();
            }
            //if (cityId !=)
            //{

            //}
            var result = AreasDataStore.CurrentAreas.Areas.FindAll(a => a.Id == id && cityId == a.CityId);
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
