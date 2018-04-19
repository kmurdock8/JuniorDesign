using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using ZXing.Net.Mobile.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProctorCreekGreenwayApp
{
    public class MapView : ContentPage
    {

        Map map;
        List<Story> storyList;
        SearchBar searchBar;

        /*
         * Generic Mapview function for both Android and IOS
         * Shown on MainPage when App is started
        */
        public MapView()
        {
            // Location of proctor creek
            var proctorCreek = new Position(33.778822, -84.439945);

            // Inititialize map centered around proctor creek
            map = new Map(
                MapSpan.FromCenterAndRadius(
                    proctorCreek, Distance.FromMiles(0.75)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };



            // Get list of all stories from central database
            InitializePins();
            string pins = map.Pins.ToString();


            // Initialize search bar functionality
            searchBar = new SearchBar { Placeholder = "Search", BackgroundColor = Xamarin.Forms.Color.White };
            searchBar.SearchButtonPressed += this.OnSearchClick;

            // Initialize QR functionality
            var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
                    ZXing.BarcodeFormat.QR_CODE
            };

            var scanner = new QRScan(options);
            scanner.OnScanResult += async (result) =>
            {
                scanner.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    scanner.readout = result.Text;
                    await Navigation.PopAsync();
                });
            };

            Button button = new Button() { Text = "QR" };
            button.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(scanner);
            };

            // GUI stuff
            var stack = new StackLayout { Spacing = 0 };
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(12, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            stack.Children.Add(searchBar);
            grid.Children.Add(map, 0, 0);
            Grid.SetRowSpan(map, 2);
            grid.Children.Add(button, 0, 1);
            stack.Children.Add(grid);
            Content = stack;
        }

        /*
         * Simple method that redirects use to a story page when a story window is clicked
         */
        async void OnLabelClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StoryPage());
        }

        // TODO: update this method to do something productive
        async void OnSearchClick(object sender, EventArgs e)
        {
            if (searchBar.Text.Equals("Culc"))
            {
                await Navigation.PushAsync(new StoryPage());
            }
        }

        public async void InitializePins()
        {
            // Get list of stories from DB
            storyList = await App.DBManager.GetStoriesAsync();

            // Loop through each story
            foreach (Story s in storyList) {
                // Get story info
                double lat = s.Lat;
                double lng = s.Long;
                string locName = s.Name;

                // Create new pin
                var pos = new Position(lat, lng);
                Pin pin = new Pin
                {
                    Label = locName,
                    Position = pos,
                };
                pin.Clicked += this.OnLabelClick;
                map.Pins.Add(pin);
            }
        }
    } 


        
    
}

