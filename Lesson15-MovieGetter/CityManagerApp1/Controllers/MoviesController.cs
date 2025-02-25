using CityManagerApp1.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CityManagerApp1.Controllers
{
    [Route("/api/[controller]")]
    public class MoviesController(IMovieSearchRepository movieSearchRepository, IArrayGeneratorRepository arrayGeneratorRepository, IArrayFilterRepository arrayFilterRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GenerateMovie([FromQuery] string searchPattern)
        {
            return Ok();
        }
    }
}
