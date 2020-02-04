﻿using System;
using System.Collections.Generic;
using System.Text;
using BarDemo.Services;
using System.Collections.ObjectModel;
using BarDemo.Models;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Linq;


namespace BarDemo.ViewModels
{
    class BarListViewModel : BaseViewModel
    {
        ObservableCollection<Business> _blist;
        public ObservableCollection<Business> Blist
        {
            get { return _blist; }
            set
            {
                _blist = value;
                OnPropertyChanged();
            }
        }

        string _latitude;
        public string Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        string _longitude;
        public string Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                SearchYelp(_latitude, _longitude, 10);
                OnPropertyChanged();
            }
        }

        public ICommand ViewMapButtonCommand { get; }
        private YelpBizSearch yelpsearch = new YelpBizSearch();

        public BarListViewModel(INavService navService) : base(navService)
        {
            _blist = new ObservableCollection<Business>();

            get_coordinates();

            Console.WriteLine(Longitude);
            Console.WriteLine(Latitude);

            //Add parameters for search
            //SearchYelp("bars", 10, "san antonio");

            //Executes when ViewMapButton is Clicked
            ViewMapButtonCommand = new Command<Business>(async (biz) => await ExecuteBarClickedCommand(biz));

        }

        public override Task Init()
        {
            throw new NotImplementedException();
        }

        // Use Yelp data service to search for businesses 
        public async void SearchYelp(string keyword, int limit, string location)
        {
            var yds = new YelpDataService(new Uri("https://api.yelp.com/v3/"));
            yelpsearch = await yds.BusinessSearch(keyword, limit, location  );

            Console.WriteLine(yelpsearch.total);

            //add businesses to observable collection to be displayed on barlist page
            for (int i = 0; i < limit; i++)
            {

                _blist.Add(yelpsearch.businesses[i]);
                _blist[i].distance = (int)yelpsearch.businesses[i].distance;
                Console.WriteLine(_blist[i].name);
                Console.WriteLine("Lattitude: " + _blist[i].coordinates.latitude);
                Console.WriteLine("Longitude: " + _blist[i].coordinates.longitude);

            }
        }

        public async void SearchYelp(string latitude, string longitude, int limit)
        {
            var yds = new YelpDataService(new Uri("https://api.yelp.com/v3/"));
            yelpsearch = await yds.BusinessSearch(latitude, longitude, limit);

            Console.WriteLine(yelpsearch.total);

            //add businesses to observable collection to be displayed on barlist page
            for (int i = 0; i < limit; i++)
            {

                _blist.Add(yelpsearch.businesses[i]);
                _blist[i].distance = (int)yelpsearch.businesses[i].distance;
                Console.WriteLine(_blist[i].name);
                Console.WriteLine("Lattitude: " + _blist[i].coordinates.latitude);
                Console.WriteLine("Longitude: " + _blist[i].coordinates.longitude);

            }
        }
        //Navigate to Map location of current bar passed in
        async Task ExecuteBarClickedCommand(Business biz)
        {
            await NavService.NavigateTo<MapViewModel, Business>(biz);
        }

        //Navigate to BarDetailViewModel; Passes in business for detail page
        public async Task ExecuteBarDetailsCommand(Business biz)
        {
            await NavService.NavigateTo<BarDetailViewModel, Business>(biz);

        }

        public async Task get_coordinates()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Console.WriteLine(position.Longitude.ToString());
            Console.WriteLine(position.Latitude.ToString());
            Latitude = position.Latitude.ToString();
            Longitude = position.Longitude.ToString();
            

            Console.WriteLine(Longitude);
            Console.WriteLine(Latitude);

            var loc_available = CrossGeolocator.Current.IsGeolocationAvailable;
            var loc_enabled = CrossGeolocator.Current.IsGeolocationEnabled;
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

            Console.WriteLine(status);
            Console.WriteLine(loc_available);
            Console.WriteLine(loc_enabled);
            if (status != PermissionStatus.Granted)
            {
                //if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                //{
                //    await Application.Current?.MainPage?.DisplayAlert("Need location", "Gunna need that location", "OK");
                //}
                CrossPermissions.Current.OpenAppSettings();
                Console.WriteLine(status);
                Console.WriteLine(loc_available);
                Console.WriteLine(loc_enabled);
            }



        }

    }
}
