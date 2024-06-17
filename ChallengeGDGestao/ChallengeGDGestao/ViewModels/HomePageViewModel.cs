using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using ChallengeGDGestao.Model;
using ChallengeGDGestao.Models;
using ChallengeGDGestao.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ChallengeGDGestao.ViewModels
{
    public class HomePageViewModel
    {
        public string ClienteInfo { get; }
        public string DataAtual { get; }
        public List<Instalacao> Instalacoes{ get; }

        public HomePageViewModel(ApiHomeResponse user, string cnpj)
        {
            ClienteInfo = $"Cliente: {cnpj}";
            DataAtual = $"Data: {DateTime.Now:dd/MM/yyyy}";
            Instalacoes = user.Result;

            ItemSelectedCommand = new Command<Instalacao>(async (instalacao) =>
            {
                if (instalacao != null)
                {
                    await CarregarFaturas(instalacao);
                }
            });
        }
        public ICommand ItemSelectedCommand { get; }


        private async Task CarregarFaturas(Instalacao instalacao)
        {
            string url = $"https://gddev.gdgestao.com.br/api/contas?numinstalacao={instalacao.NumInstalacao}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ApiInstallationResponse>(responseData);

                        if (apiResponse != null && apiResponse.Sucesso)
                        {
                            var installationPageViewModel = new InstallationViewModel(apiResponse, instalacao);
                            await Application.Current.MainPage.Navigation.PushAsync(new InstallationPage(installationPageViewModel));
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Erro", apiResponse.MsgErro, "OK");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Falha ao buscar dados da instalação.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
                }
            }
        }
    }
}
