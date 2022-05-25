using Grupo8.SoySano.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Grupo8.SoySano.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}