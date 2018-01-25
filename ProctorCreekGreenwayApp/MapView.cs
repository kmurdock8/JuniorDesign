using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProctorCreekGreenwayApp
{
    public class MapView : ContentPage
    {
        public MapView()
        {
            // Create map centered at the Culc
            Map map = new Map(
                MapSpan.FromCenterAndRadius(
                    new Position(33.774754, -84.396322), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
        }
    }
}

