using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.Services;

namespace SignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController(IFileService fileService) : ControllerBase
    {
        [HttpGet]
        public async Task<double> Get()
        {
            return await fileService.Read();
        }

        [HttpGet("Room")]
        public async Task<double> Get(string room)
        {
            var data = await fileService.Read(room);
            return data;
        }

        [HttpGet("IncreaseRoom")]
        public async Task<ActionResult> Increase(string room, double data)
        {
            var result = await fileService.Read(room);
            result = result + data;
            await fileService.Write(room, result);
            return Ok(result);
        }
        [HttpGet("Increase")]
        public async Task<ActionResult> Increase(double data)
        {
            var result = await fileService.Read();
            result = result + data;
            await fileService.Write(result);
            return Ok(result);
        }
    }
}
