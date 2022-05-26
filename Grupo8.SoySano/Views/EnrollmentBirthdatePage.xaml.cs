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
    public partial class EnrollmentBirthdatePage : ContentPage
    {
        public EnrollmentBirthdatePage()
        {
            InitializeComponent();
        }

        private async void btnContinue_Clicked(object sender, EventArgs e)
        {
            AppHelpers.CurrentUser.BirthDate = dtpBirthdate.Date;
            await Shell.Current.GoToAsync(nameof(EnrollmentMeasuresPage));
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(EnrollmentGenderPage));
        }
    }
}