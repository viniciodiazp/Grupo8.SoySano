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
using System.Collections.ObjectModel;

namespace Grupo8.SoySano.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivitiesPage : ContentPage
    {
        private ActivityService service;
        public ActivitiesPage()
        {
            InitializeComponent();
            this.service = new ActivityServiceRestImpl();
            this.lblUsuario.Text = string.Format("Hola, {0}", AppHelpers.CurrentUser.Name);
            LoadActivities();
        }


        private void LoadActivities()
        {
            List<Activity> activities = this.service.GetAll(AppHelpers.CurrentUser);
            if (activities == null)
            {
                this.lbTitle.Text = "Sin Actividades";
                this.lstActivities.ItemsSource = null;
            }
            else
            {
                this.lbTitle.Text = "Mis Actividades";
                ObservableCollection<Activity> data = new ObservableCollection<Activity>(activities);
                this.lstActivities.ItemsSource = data;
            }
        }

        private async void lstActivities_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Activity activity = (Activity)e.SelectedItem;
            AppHelpers.CurrentActivity = activity;
            await Shell.Current.GoToAsync(nameof(ActivityDetailPage));

        }
    }
}