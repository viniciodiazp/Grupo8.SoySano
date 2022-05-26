using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Grupo8.SoySano.Models;
using Grupo8.SoySano.Services;
using Grupo8.SoySano.Api;
namespace Grupo8.SoySano.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityDetailPage : ContentPage
    {
        private Activity activity;
        private ActivityService service;
        public ActivityDetailPage()
        {
            InitializeComponent();
            this.activity = AppHelpers.CurrentActivity;
            this.service = new ActivityServiceRestImpl();
            this.lblUsuario.Text = string.Format("Hola, {0}", AppHelpers.CurrentUser.Name);
            LoadActivity();
        }

        private void LoadActivity()
        {
            this.lbActivityName.Text = this.activity.Name;
            this.lbDate.Text = this.activity.StartDate.ToString("dddd, dd MMMM yyyy");
            this.imgPhoto.Source = this.activity.Photo;
            this.lbStartDate.Text = this.activity.StartDate.ToString("HH:mm:ss");
            this.lbFinishDate.Text = this.activity.FinishDate.ToString("HH:mm:ss");
            this.lbDuration.Text = this.activity.Duration;
            this.lbCalories.Text = string.Format("{0:0.00}", this.activity.Calories);
        }
    }
}