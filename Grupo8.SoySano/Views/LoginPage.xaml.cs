using Grupo8.SoySano.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grupo8.SoySano.Services;
using Grupo8.SoySano.Models;
using Grupo8.SoySano.Api;
namespace Grupo8.SoySano.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage, IGoogleAuthenticationDelegate
    {
        private UserService service;

        private IGoogleAuthService _googleAuthService;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            _googleAuthService = DependencyService.Resolve<IGoogleAuthService>();
            this.service = new UserServiceRestImpl();
        }
        
        public void OnAuthenticationCanceled()
        {
            lbAccountInfo.Text = "Authentication canceled";
        }

        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            GoogleAccountInfoService googleService = new GoogleAccountInfoService();
            lbAccountInfo.Text = await googleService.GetEmailAsync(token.TokenType, token.AccessToken);
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            lbAccountInfo.Text = "Authentication failed";
        }

        private void btnGoogleLogin_Clicked(object sender, EventArgs e)
        {
            _googleAuthService.Autheticate(this);
        }

        private async void btnGoogleLoginAlternate_Clicked(object sender, EventArgs e)
        {
            User user = new User();
            user.Id = 1;
            AppHelpers.CurrentUser = user;
            await Shell.Current.GoToAsync(nameof(EnrollmentGenderPage));
        }

        private async void btnExistingUser_Clicked(object sender, EventArgs e)
        {
            User user = this.service.GetById(1);
            AppHelpers.CurrentUser = user;
            await Shell.Current.GoToAsync(nameof(ActivitiesPage));
        }
    }
}