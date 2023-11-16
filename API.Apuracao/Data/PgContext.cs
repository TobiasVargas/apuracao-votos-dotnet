using API.Apuracao.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Apuracao.Data
{
    public class PgContext : DbContext
    {
        public PgContext(DbContextOptions<PgContext> options) : base(options) { }
        public DbSet<Voto> Votos { get; set; }
    }
}
