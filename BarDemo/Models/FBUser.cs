using System;
using Newtonsoft.Json;

namespace BarDemo.Models
{
    [JsonObject]
    public class FBUser
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string DoB { get; set; }
        public AgeRange age_range { get; set; }
    }

    [JsonObject]
    public class AgeRange
    {
        public int min { get; set; }
        public int max { get; set; }
    }

}