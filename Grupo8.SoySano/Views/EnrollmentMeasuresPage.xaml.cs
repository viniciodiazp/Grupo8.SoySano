using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grupo8.SoySano.Models;
using Grupo8.SoySano.Api;
using Grupo8.SoySano.Utils;
using Grupo8.SoySano.Services;

namespace Grupo8.SoySano.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnrollmentMeasuresPage : ContentPage
    {
        private UserService service;
        public EnrollmentMeasuresPage()
        {
            InitializeComponent();
            Shell.Current.Title = "Medidas";
            this.service = new UserServiceRestImpl();
        }

        private async void btnContinue_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHeight.Text))
            {
                DisplayAlert(Constant.Messages.DISPLAY_TITLE, "Digite estatura", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(txtWeight.Text))
            {
                DisplayAlert(Constant.Messages.DISPLAY_TITLE, "Digite peso", "Aceptar");
                return;
            }

            User user = AppHelpers.CurrentUser;
            user.Height = Convert.ToDouble(txtHeight.Text);
            user.Weight = Convert.ToDouble(txtWeight.Text);
            User responseEntity = this.service.Create(user);
            AppHelpers.CurrentUser = responseEntity;
            await DisplayAlert(Constant.Messages.DISPLAY_TITLE,
                "Se creó el usuario " + responseEntity.Id.ToString(), "Aceptar");
            await Shell.Current.GoToAsync(nameof(ActivitiesPage));
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(EnrollmentBirthdatePage));
        }
    }
}