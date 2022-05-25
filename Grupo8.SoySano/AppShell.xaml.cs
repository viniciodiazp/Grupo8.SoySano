using Grupo8.SoySano.ViewModels;
using Grupo8.SoySano.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Grupo8.SoySano
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(EnrollmentGenderPage), typeof(EnrollmentGenderPage));
            Routing.RegisterRoute(nameof(EnrollmentMeasuresPage), typeof(EnrollmentMeasuresPage));
            Routing.RegisterRoute(nameof(EnrollmentBirthdatePage), typeof(EnrollmentBirthdatePage));
            Routing.RegisterRoute(nameof(ActivitiesPage), typeof(ActivitiesPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(ActivityDetailPage), typeof(ActivityDetailPage));
            Routing.RegisterRoute(nameof(ActivityRegisterPage), typeof(ActivityRegisterPage));

            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
