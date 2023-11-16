using API.Jogadores.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Jogadores.Data
{
    public class PgContext : DbContext
    {
        public PgContext(DbContextOptions<PgContext> options) : base(options) { }

        public DbSet<Jogador> Jogadores { get; set; }
    }
}
