using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using BarDemo.Models;
using BarDemo.ViewModels;
using BarDemo.Services;
using System.Diagnostics;
using Xamarin.Auth;
using System.Linq;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.ComponentModel;
using System.Collections.Generic;

namespace BarDemo.Views
{
    public partial class MapPage : ContentPage
    {

        MapViewModel _vm
        {
            get { return BindingContext as MapViewModel; }
        }

        public MapPage()
        {
            InitializeComponent();

            BindingContext = new MapViewModel(DependencyService.Get<INavService>());
        }


        void UpdateMap()
        {
            if (_vm.Bar == null)
            {
                return;
            }


            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_vm.Bar.coordinates.latitude, _vm.Bar.coordinates.longitude), Distance.FromMiles(2)));
            // Place a pin on the map for the log entry's location
            customMap.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = _vm.Bar.name,
                Position = new Position(_vm.Bar.coordinates.latitude, _vm.Bar.coordinates.longitude)
            });


        }

        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(MapViewModel.Bar))
            {
                UpdateMap();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_vm != null)
            {
                _vm.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (_vm != null)
            {
                _vm.PropertyChanged -= OnViewModelPropertyChanged;
            }
        }



    }
}
