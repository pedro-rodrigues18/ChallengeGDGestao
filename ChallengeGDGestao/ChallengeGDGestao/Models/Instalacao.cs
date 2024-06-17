using System.Collections.Generic;

namespace ChallengeGDGestao.Model
{
    public class Instalacao
    {
        public string NumInstalacao { get; set; }
        public string Endereco { get; set; }
        public List<Fatura> Faturas { get; set; }
    }
}
