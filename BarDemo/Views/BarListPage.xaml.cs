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


            //Items = new ObservableCollection<string>
            //{
            //    "Item 1",
            //    "Item 2",
            //    "Item 3",
            //    "Item 4",
            //    "Item 5"
            //};

            //MyListView.ItemsSource = Items;

            if (IsBusy)
                return;
            IsBusy = true;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var bar = (Business)e.Item;
            _vm.BarCommand.Execute(bar);

            //await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            //((ListView)sender).SelectedItem = null;
            MyListView.SelectedItem = null;
        }

    }
}
