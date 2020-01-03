using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BarDemo.Services;
using BarDemo.Models;
using System.Threading;


namespace BarDemo.ViewModels
{
    class BarDetailViewModel : BaseViewModel<Business>
    {

        Business _bar;
        public Business Bar
        {
            get { return _bar; }
            set { _bar = value; }
        }

        BizDetails _bizDetails;
        public BizDetails BizDetails
        {
            get { return _bizDetails; }
            set
            {
                _bizDetails = value;
                OnPropertyChanged();
            }
        }

        string _bizName;
        public string BizName
        {
            get { return _bizName; }
            set
            {
                _bizName = value;
                OnPropertyChanged();
            }
        }

        public BarDetailViewModel(INavService navService) : base(navService)
        {

        }


        public override Task Init()
        {
            throw new NotImplementedException();
        }

        public override async Task Init(Business biz)
        {
            _bar = biz;
            string id = Bar.id;
            _bizDetails = new BizDetails();
            YelpDataService yds = new YelpDataService(new Uri("https://api.yelp.com/v3/"));
            _bizDetails = await yds.BusinessSearch(id);
            BizName = _bizDetails.name;


        }


    }
}
