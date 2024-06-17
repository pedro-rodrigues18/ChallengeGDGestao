using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeGDGestao.Model
{
    public class ApiHomeResponse
    {
        public bool Sucesso { get; set; }
        public string MsgErro { get; set; }
        public List<Instalacao> Result { get; set; } 
    }
}