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
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly IParkopolisRepository _repository;
        private readonly IMapper _mapper;


        public CitiesController(IParkopolisRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetCities()
        {
            
            return Ok(_mapper.Map<IEnumerable<CityDto>>(_repository.GetAllCities()));
        }

        [HttpGet("{cityId}")]
        public IActionResult GetCity(int cityId)
        {
            if (!_repository.CityExists(cityId)) return NotFound();

            return Ok(_mapper.Map<CityDto>(_repository.GetCityById(cityId)));
        }

        [HttpPost] 
        public IActionResult AddCity([FromBody] City cityToAdd)
        {
            _repository.AddCity(cityToAdd);
            _repository.Save();
            return NoContent();
        }

        [HttpDelete("{cityId}")]
        public IActionResult RemoveCity(int cityId)
        {
           if(!_repository.CityExists(cityId))
            {
                return NotFound();
            }

            var city = _repository.GetCityById(cityId);
            _repository.RemoveCity(city);
            return NoContent();
        }
    }
}
