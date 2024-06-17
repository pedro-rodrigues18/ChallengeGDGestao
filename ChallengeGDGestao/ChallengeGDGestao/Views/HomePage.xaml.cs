using ChallengeGDGestao.ViewModels;
using Xamarin.Forms;

namespace ChallengeGDGestao
{
    public partial class HomePage : ContentPage
    {
        private HomePageViewModel viewModel;

        public HomePage(HomePageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            viewModel.ItemSelectedCommand.Execute(e.SelectedItem as Model.Instalacao);
        }
    }
}
