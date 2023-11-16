using System.ComponentModel.DataAnnotations.Schema;

namespace API.Apuracao.Entity
{
    [Table(name: "votos", Schema = "apuracao")]
    public class Voto
    {
        public long Id { get; set; }
        public string NomeJogador {  get; set; }
        public string TimeJogador { get; set; }
        public int CamisaJogador { get; set; }
        public long IdJogador { get; set; }
    }
}
