using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Parkopolis.API.MockData;
using Parkopolis.API.Models;
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

        [HttpGet("{cityId}")]
        public IActionResult GetCity(int cityId)
        {
            if (!Validation.CityExists(cityId)) return NotFound();

            return Ok(CitiesDataStore.CurrentCities.Cities
                .FirstOrDefault(c => c.Id == cityId));
        }

        [HttpPost]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult CreateCity([FromBody] CityDto city)
        {
            var maxCitytId = CitiesDataStore.CurrentCities.Cities.Max(c => c.Id);

            var newCity = new CityDto()
            {
                Id = maxCitytId + 1,
                Name = city.Name
            };
            CitiesDataStore.CurrentCities.Cities.Add(newCity);

            return NoContent();
        }
    }
}
