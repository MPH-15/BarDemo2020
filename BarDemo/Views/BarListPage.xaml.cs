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
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;




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
            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.LocationWhenInUse);
                //if (!hasPermission)
                //    return;

                //ButtonGetGPS.IsEnabled = false;

                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                //labelGPS.Text = "Getting gps...";

                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null);

                if (position == null)
                {
                    //labelGPS.Text = "null gps :(";
                    return;
                }
                //savedPosition = position;
                //ButtonAddressForPosition.IsEnabled = true;
                Console.WriteLine(string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                    position.Timestamp, position.Latitude, position.Longitude,
                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed));
                _vm.Latitude = position.Latitude.ToString();
                _vm.Longitude = position.Longitude.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
            }

        }

    }
}
