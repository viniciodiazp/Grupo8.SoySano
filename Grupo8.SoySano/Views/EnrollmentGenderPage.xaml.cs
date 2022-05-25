using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grupo8.SoySano.Models;

namespace Grupo8.SoySano.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnrollmentGenderPage : ContentPage
    {
        private User user;
        public EnrollmentGenderPage()
        {
            InitializeComponent();
        }

        private void btnMale_Clicked(object sender, EventArgs e)
        {
            AppHelpers.CurrentUser.Gender = "M";
            NextPage();
        }

        private void btnFemale_Clicked(object sender, EventArgs e)
        {
            AppHelpers.CurrentUser.Gender = "F";
            NextPage();
        }

        private void btnOther_Clicked(object sender, EventArgs e)
        {
            AppHelpers.CurrentUser.Gender = "O";
            NextPage();
        }

        private async void NextPage()
        {
            await Shell.Current.GoToAsync(nameof(EnrollmentBirthdatePage));
        }
    }
}