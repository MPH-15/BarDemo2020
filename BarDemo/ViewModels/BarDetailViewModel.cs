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

        DateTime _dt;
        public DateTime Dt
        {
            get { return _dt; }
            set
            {
                _dt = value;
                OnPropertyChanged();
            }
        }

        string _dayofweek;
        public string DayOfWeek
        {
            get { return _dayofweek; }
            set
            {
                _dayofweek = value;
                OnPropertyChanged();
            }
        }

        string _hoursStart;
        public string HoursStart
        {
            get { return _hoursStart; }
            set
            {
                _hoursStart = value;
                OnPropertyChanged();
            }
        }

        string _hoursEnd;
        public string HoursEnd
        {
            get { return _hoursEnd; }
            set
            {
                _hoursEnd = value;
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
            BizDetails = new BizDetails();
            YelpDataService yds = new YelpDataService(new Uri("https://api.yelp.com/v3/"));
            BizDetails = await yds.BusinessSearch(id);
            BizName = BizDetails.name;
            get_hour_details();
            //Console.WriteLine(_dt.DayOfWeek);
            Console.WriteLine(DayOfWeek);
            Console.WriteLine(HoursStart);
            Console.WriteLine(HoursEnd);
            DayOfWeek = "Monday";


        }

        public void get_hour_details()
        {
            switch (Dt.DayOfWeek.ToString())
            {
                case "Monday":
                    HoursStart = BizDetails.hours[0].open[1].start;
                    Console.WriteLine(HoursStart);
                    HoursEnd = BizDetails.hours[0].open[1].end;
                    Console.WriteLine(HoursEnd);
                    DayOfWeek = "Monday";

                    break;

                default:
                    HoursStart = BizDetails.hours[0].open[0].start;
                    HoursEnd = BizDetails.hours[0].open[0].end;
                    DayOfWeek = "Other";
                    Console.WriteLine(Dt.DayOfWeek.ToString());
                    break;

            }
        }


    }
}
