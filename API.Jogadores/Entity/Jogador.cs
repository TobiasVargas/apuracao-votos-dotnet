using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Jogadores.Entity
{
    [Table(name: "jogadores", Schema = "jogadores")]
    [PrimaryKey("Id")]
    public class Jogador
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("time")]
        public string Time { get; set; }
        [Column("camisa")]
        public int Camisa { get; set; }
    }
}
