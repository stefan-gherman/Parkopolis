using Microsoft.AspNetCore.Mvc;
using Parkopolis.API.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkopolis.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.CurrentCities.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int cityId)
        {
            if (!Validation.CityExists(cityId)) return NotFound();

            return Ok(CitiesDataStore.CurrentCities.Cities
                .FirstOrDefault(c => c.Id == cityId));
        }
    }
}
