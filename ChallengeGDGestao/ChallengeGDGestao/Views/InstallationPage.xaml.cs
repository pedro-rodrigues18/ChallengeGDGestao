using ChallengeGDGestao.ViewModels;
using Xamarin.Forms;

namespace ChallengeGDGestao.Views
{
    public partial class InstallationPage : ContentPage
    {
        public InstallationPage(InstallationViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}

