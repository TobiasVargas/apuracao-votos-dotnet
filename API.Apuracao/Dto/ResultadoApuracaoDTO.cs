using System.Runtime.InteropServices;

namespace API.Apuracao.Dto
{
    public class ResultadoApuracaoDTO
    {
        public long QuantidadeVotos { get; set; }
        public long IdJogador { get; set; }
        public string NomeJogador { get; set; }
        public int CamisaJogador { get; set; }
        public string TimeJogador { get; set; }
    }
}
