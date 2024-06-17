using ChallengeGDGestao.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeGDGestao.Models
{
    public class ApiInstallationResponse
    {
        public bool Sucesso { get; set; }
        public string MsgErro { get; set; }
        public List<Fatura> Result { get; set; }
    }
}
