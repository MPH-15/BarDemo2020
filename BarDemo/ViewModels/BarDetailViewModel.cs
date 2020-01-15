using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BarDemo.Services;
using BarDemo.Models;
using System.Threading;
using System.Collections.ObjectModel;

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

        YelpDataService yds = new YelpDataService(new Uri("https://api.yelp.com/v3/"));

        BizReviews _br;
        public BizReviews BR
        {
            get { return _br; }
            set { _br = value; }
        }

        ObservableCollection<Review> _reviews;
        public ObservableCollection<Review> Reviews
        {
            get { return _reviews; }
            set
            {
                _reviews = value;
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
            Bar = biz;
            string id = Bar.id;

            await get_bar_details(id);
            get_hour_details();
            await get_bar_reviews();

        }

        public async Task get_bar_details(string id)
        {
            BizDetails = new BizDetails();
            BizDetails = await yds.BusinessSearch(id);
            BizName = BizDetails.name;

        }

        public void get_hour_details()
        {
            long t = DateTime.Now.Ticks;
            Dt = new DateTime(t);

            switch (Dt.DayOfWeek.ToString())
            {
                case "Sunday":
                    HoursStart = BizDetails.hours[0].open[0].start;
                    HoursEnd = BizDetails.hours[0].open[0].end;
                    DayOfWeek = "Sunday";
                    break;

                case "Monday":
                    HoursStart = BizDetails.hours[0].open[1].start;
                    HoursEnd = BizDetails.hours[0].open[1].end;
                    DayOfWeek = "Monday";
                    break;

                case "Tuesday":
                    HoursStart = BizDetails.hours[0].open[2].start;
                    HoursEnd = BizDetails.hours[0].open[2].end;
                    DayOfWeek = "Tuesday";
                    break;

                case "Wednesday":
                    HoursStart = BizDetails.hours[0].open[3].start;
                    HoursEnd = BizDetails.hours[0].open[3].end;
                    DayOfWeek = "Wednesday";
                    break;

                case "Thursday":
                    HoursStart = BizDetails.hours[0].open[4].start;
                    HoursEnd = BizDetails.hours[0].open[4].end;
                    DayOfWeek = "Thursday";
                    break;

                case "Friday":
                    HoursStart = BizDetails.hours[0].open[5].start;
                    HoursEnd = BizDetails.hours[0].open[5].end;
                    DayOfWeek = "Friday";
                    break;

                case "Saturday":
                    HoursStart = BizDetails.hours[0].open[6].start;
                    HoursEnd = BizDetails.hours[0].open[6].end;
                    DayOfWeek = "Saturday";
                    break;


            }
        }

        public async Task get_bar_reviews()
        {
            try
            {
                BR = new BizReviews();
                Reviews = new ObservableCollection<Review>();
                BR = await yds.BusinessSearch(Bar.id, "reviews");
            }
            catch
            {
                Console.WriteLine("Issue with YDS");
            }
            foreach (var review in BR.reviews)
            {
                
                _reviews.Add(review);
                //Console.WriteLine(review.text);
            }
            Console.WriteLine(Reviews[0].text);
            Console.WriteLine(Reviews[1].text);
            Console.WriteLine(Reviews[2].text);
        }


    }
}
