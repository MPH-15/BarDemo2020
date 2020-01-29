using System;
using System.Collections.Generic;
using System.Text;

namespace BarDemo.Models
{
    public class User
    {
        public string id { get; set; }
        public string profile_url { get; set; }
        public string image_url { get; set; }
        public string name { get; set; }
    }

    public class Review
    {
        public string id { get; set; }
        public int rating { get; set; }
        public User user { get; set; }
        public string text { get; set; }
        public string time_created { get; set; }
        public string url { get; set; }
    }

    public class BizReviews
    {
        public List<Review> reviews { get; set; }
        public int total { get; set; }
        public List<string> possible_languages { get; set; }
    }
}
