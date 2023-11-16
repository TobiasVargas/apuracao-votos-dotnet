using API.Jogadores.Data;
using API.Jogadores.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Jogadores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogadoresController : ControllerBase
    {
        private PgContext _context;

        public JogadoresController(PgContext context)
        {
            _context = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Get()
        {
            List<Jogador> jogadores = _context.Jogadores.AsNoTracking().ToList();
            return Ok(jogadores);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var jogador = await _context.Jogadores.FirstOrDefaultAsync(j => j.Id.Equals(id));
            if (jogador == null)
            {
                return NotFound();
            }
            return Ok(jogador);
        }
    }
}
