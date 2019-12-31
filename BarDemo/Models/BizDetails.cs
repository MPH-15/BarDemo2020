using System;
using System.Collections.Generic;
using System.Text;

namespace BarDemo.Models
{
    public class BizDetails
    {

            public string id { get; set; }
            public string alias { get; set; }
            public string name { get; set; }
            public string image_url { get; set; }
            public bool is_claimed { get; set; }
            public bool is_closed { get; set; }
            public string url { get; set; }
            public string phone { get; set; }
            public string display_phone { get; set; }
            public int review_count { get; set; }
            public List<CategoryDetails> categories { get; set; }
            public double rating { get; set; }
            public LocationDetails location { get; set; }
            public CoordinatesDetails coordinates { get; set; }
            public List<string> photos { get; set; }
            public string price { get; set; }
            public List<Hour> hours { get; set; }
            public List<object> transactions { get; set; }
            public List<SpecialHour> special_hours { get; set; }
        
    }
    public class CategoryDetails
    {
        public string alias { get; set; }
        public string title { get; set; }
    }

    public class LocationDetails
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string zip_code { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public List<string> display_address { get; set; }
        public string cross_streets { get; set; }
    }

    public class CoordinatesDetails
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Open
    {
        public bool is_overnight { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public int day { get; set; }
    }

    public class Hour
    {
        public List<Open> open { get; set; }
        public string hours_type { get; set; }
        public bool is_open_now { get; set; }
    }

    public class SpecialHour
    {
        public string date { get; set; }
        public object is_closed { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool is_overnight { get; set; }
    }


}
