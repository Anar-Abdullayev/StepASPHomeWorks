using AutoMapper;
using CityManagerApp1.Dtos;
using CityManagerApp1.Entities;
using CityManagerApp1.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityManagerApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly IAppRepository _appRepository;
        private readonly IMapper _mapper;
        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCities(int id)
        {
            var items = await _appRepository.GetCitiesAsync(id);
            var dtos = _mapper.Map<IEnumerable<CityForListDto>>(items);

            return Ok(dtos);
        }

        // POST api/<CitiesController>
        [HttpPost]
        public async Task<ActionResult<CityDto>> Post([FromBody] CityDto dto)
        {
            var entity = _mapper.Map<City>(dto);
            await _appRepository.AddAsync(entity);
            await _appRepository.SaveAllAsync();

            var returnedDto=_mapper.Map<CityDto>(entity);
            return Ok(returnedDto);
        }

    }
}
