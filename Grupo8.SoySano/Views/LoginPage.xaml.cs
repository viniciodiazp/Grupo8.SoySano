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
using Grupo8.SoySano.Utils;

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
            ShowButtons(false);
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
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                await DisplayAlert(Constant.Messages.DISPLAY_TITLE, "Digite email", "Aceptar");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                await DisplayAlert(Constant.Messages.DISPLAY_TITLE, "Digite nombre", "Aceptar");
                txtUserName.Focus();
                return;
            }
            User user = new User();
            user.Id = 0;
            user.Email = txtEmail.Text;
            user.Name = txtUserName.Text;
            AppHelpers.CurrentUser = user;
            await Shell.Current.GoToAsync(nameof(EnrollmentGenderPage));
        }

        private async void btnExistingUser_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserId.Text))
            {
                await DisplayAlert(Constant.Messages.DISPLAY_TITLE, "Digite Id", "Aceptar");
                txtUserId.Focus();
                return;
            }
            User user = this.service.GetById(Convert.ToInt32(txtUserId.Text));
            AppHelpers.CurrentUser = user;
            await Shell.Current.GoToAsync(nameof(ActivitiesPage));
        }

        private void chkAlternate_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ShowButtons(e.Value);
        }

        private void ShowButtons(bool show)
        {
            btnExistingUser.IsVisible = show;
            txtEmail.IsVisible = show;
            txtUserId.IsVisible = show;
            txtUserName.IsVisible = show;
            btnGoogleLoginAlternate.IsVisible = show;
        }

    }
}