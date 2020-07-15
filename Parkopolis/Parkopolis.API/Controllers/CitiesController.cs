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
        public IActionResult GetCity(int id)
        {
            // find city
            var cityToReturn = CitiesDataStore.CurrentCities.Cities
                .FirstOrDefault(c => c.Id == id);

            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}
