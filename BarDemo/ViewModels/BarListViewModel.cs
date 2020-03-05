using System;
using System.Collections.Generic;
using System.Text;
using BarDemo.Services;
using System.Collections.ObjectModel;
using BarDemo.Models;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using System.Diagnostics;

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



        public ICommand ViewMapButtonCommand { get; }
        private YelpBizSearch yelpsearch = new YelpBizSearch();

        public BarListViewModel(INavService navService) : base(navService)
        {
            MessagingCenter.Subscribe<ProfileViewModel, int>(this, "RadiusSliderValue", async (sender, arg) =>
            {

                Debug.WriteLine("BarListViewModel: In Messaging Center");
                Debug.WriteLine("BLVM Radius: " + arg);

            });



            _blist = new ObservableCollection<Business>();

            //Add parameters for search
            SearchYelp("bars", 10, "san antonio");

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


    }
}
