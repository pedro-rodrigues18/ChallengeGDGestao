using ChallengeGDGestao.Model;
using ChallengeGDGestao.Models;

namespace ChallengeGDGestao.ViewModels
{
    public class InstallationViewModel
    {
        public Instalacao Instalacao { get; }

        public InstallationViewModel(ApiInstallationResponse apiResponse, Instalacao instalacao)
        {
            Instalacao = instalacao;
            Instalacao.Faturas = apiResponse.Result;
        }
    }
}

