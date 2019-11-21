using System;
using System.Collections.Generic;
using System.Text;
using BarDemo.Services;
using System.Collections.ObjectModel;
using BarDemo.Models;
using System.Threading.Tasks;

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
        private YelpBizSearch yelpsearch = new YelpBizSearch();

        public BarListViewModel(INavService navService) : base(navService)
        {
            _blist = new ObservableCollection<Business>();

            SearchYelp();
           
            
        }

        public override Task Init()
        {
            throw new NotImplementedException();
        }

        public async void SearchYelp()
        {
            var yds = new YelpDataService(new Uri("https://api.yelp.com/v3/"));
            yelpsearch = await yds.BusinessSearch();
            //blist = yelpsearch.businesses;

            Console.WriteLine(yelpsearch.total);


            for (int i = 0; i < 5; i++)
            {

                //blist[i].name = yelpsearch.businesses[i].name;
                _blist.Add(new Business
                {
                    name = yelpsearch.businesses[i].name
                });
                Console.WriteLine(_blist[i].name);

            }


        }
    }
}
