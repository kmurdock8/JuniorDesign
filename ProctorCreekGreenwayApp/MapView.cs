using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProctorCreekGreenwayApp
{
    public class MapView : ContentPage
    {
        /*
         * Generic Mapview function for both Android and IOS
         * Shown on MainPage when App is started
        */

        public MapView()
        {
            // Create map centered at the Culc
            var position = new Position(33.774754, -84.396322);

            Map map = new Map(
                MapSpan.FromCenterAndRadius(
                    position, Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            Pin pin = new Pin();
            pin.Label = "Clough Undergraduate Learning Commons";
            pin.Position = position;
            map.Pins.Add(pin);

            var searchBar = new SearchBar { Placeholder = "Search", BackgroundColor = Xamarin.Forms.Color.White };
            searchBar.SearchButtonPressed += (object sender, EventArgs e) => {
                if (searchBar.Text.Equals("Culc")) {
                    Navigation.PushAsync(new StoryPage());
                }
            };

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(searchBar);
            stack.Children.Add(map);
            Content = stack;
        }
    }
}

