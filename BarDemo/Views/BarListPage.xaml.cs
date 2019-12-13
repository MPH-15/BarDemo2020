using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BarDemo.ViewModels;
using BarDemo.Services;
using BarDemo.Models;


namespace BarDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarListPage : ContentPage
    {
        //public ObservableCollection<string> Items { get; set; }

        BarListViewModel _vm
        {
            get { return BindingContext as BarListViewModel; }
        }

        public BarListPage()
        {
            InitializeComponent();
            BindingContext = new BarListViewModel(DependencyService.Get<INavService>());


            if (IsBusy)
                return;
            IsBusy = true;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            // Initate opening for BarDetailPage passing current bar as argument
            var bar = (Business)e.Item;
            await _vm.ExecuteBarDetailsCommand(bar);
            //_vm.BarCommand.Execute(bar);

        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            //if (e.Item == null)
            //    return;

            //var bar = (Business)e.Item;
            //_vm.BarCommand.Execute(bar);

            await DisplayAlert("Item Tapped", "A map button was tapped.", "OK");

            //Deselect Item
            //((ListView)sender).SelectedItem = null
        }
    }
}
