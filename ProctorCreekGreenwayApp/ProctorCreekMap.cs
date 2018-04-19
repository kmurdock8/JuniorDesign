using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProctorCreekGreenwayApp
{
    public class ProctorCreekMap : Map
    {
        public List<ProctorCreekPin> ProctorCreekPins { get; set; }

        public ProctorCreekMap(MapSpan m) : base(m) => ProctorCreekPins = new List<ProctorCreekPin>();
    }

    public class ProctorCreekPin : Pin
    {
        public Story story;
    }
}

