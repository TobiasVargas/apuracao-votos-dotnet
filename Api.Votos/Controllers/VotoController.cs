using Api.Votos.Dto;
using Api.Votos.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace Api.Votos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        [Route("")]
        [HttpPost]
        public IActionResult Post([FromBody] VotoDTO votoDTO)
        {
            var producer = new KafkaProducer();
            producer.Send(votoDTO);
            return Ok();
        }
    }
}
