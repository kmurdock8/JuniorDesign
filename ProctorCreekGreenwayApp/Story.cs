using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProctorCreekGreenwayApp
{
    public class Story
    {
        public string Created_date { get; set; }
        public string Details { get; set; }
        public int ID { get; set; }
        public List<string> Images { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Modified_date { get; set; }
        public string Name { get; set; }
        public string Resource_uri { get; set; }
    }
}
