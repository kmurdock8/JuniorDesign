﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProctorCreekGreenwayApp
{
    public class ProctorCreekMap : Map
    {
        public ProctorCreekMap() : base() {
            
        }
        public ProctorCreekMap(MapSpan m) : base(m) {
            
        }


    }

    public class ProctorCreekPin : Pin
    {
        public Story story;
        public string ImageURL;
    }
}

