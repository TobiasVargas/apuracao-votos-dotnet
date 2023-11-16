using API.Apuracao.Data;
using API.Apuracao.Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Apuracao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApuracaoController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public IActionResult Get([FromServices] PgContext _pgContext)
        {
            var query = _pgContext.Votos
                .GroupBy(v => new {v.IdJogador, v.CamisaJogador, v.NomeJogador, v.TimeJogador})
                .Select(g => new ResultadoApuracaoDTO
                {
                    QuantidadeVotos = g.Count(),
                    IdJogador = g.Key.IdJogador,
                    CamisaJogador = g.Key.CamisaJogador,
                    NomeJogador = g.Key.NomeJogador,
                    TimeJogador = g.Key.TimeJogador
                });
            List<ResultadoApuracaoDTO> apuracoes = query.ToList();
            return Ok(apuracoes);
        }
    }
}
