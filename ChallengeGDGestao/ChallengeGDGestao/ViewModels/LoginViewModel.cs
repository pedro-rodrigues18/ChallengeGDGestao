using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using ChallengeGDGestao.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ChallengeGDGestao.ViewModels
{
    public class LoginViewModel
    {
        private string cpfCnpj;
        public string CpfCnpj
        {
            set
            {
                cpfCnpj = value;
            }
            get
            {
                return cpfCnpj;
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }

        private async Task Login()
        {
            if (string.IsNullOrEmpty(CpfCnpj))
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Preencha o campo CPF/CNPJ", "OK");
                return;
            }

            string url = $"https://gddev.gdgestao.com.br/api/home?cpfcnpj={CpfCnpj}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ApiHomeResponse>(responseData);

                        if (apiResponse != null && apiResponse.Sucesso)
                        {
                            var homePageViewModel = new HomePageViewModel(apiResponse, CpfCnpj);
                            
                            await Application.Current.MainPage.Navigation.PushAsync(new HomePage(homePageViewModel));
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Erro", "Falha ao fazer login. Verifique o CPF/CNPJ.", "OK");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Erro", "Falha ao fazer login. Por favor, tente novamente.", "OK");
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
