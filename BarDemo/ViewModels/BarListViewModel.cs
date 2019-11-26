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

            //Add parameters for search
            SearchYelp("bars", 10, "san antonio");
           
            
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
                Console.WriteLine(_blist[i].name);

            }


        }
    }
}
