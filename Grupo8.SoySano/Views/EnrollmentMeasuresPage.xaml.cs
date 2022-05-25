using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupo8.SoySano.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnrollmentMeasuresPage : ContentPage
    {
        public EnrollmentMeasuresPage()
        {
            InitializeComponent();
            Shell.Current.Title = "Medidas";
        }

        private async void btnContinue_Clicked(object sender, EventArgs e)
        {
            
            await Shell.Current.GoToAsync(nameof(ItemDetailPage));
            
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {

        }
    }
}