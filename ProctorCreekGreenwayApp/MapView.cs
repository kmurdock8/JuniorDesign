using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using ZXing.Net.Mobile.Forms;
using System.Collections.Generic;

namespace ProctorCreekGreenwayApp
{
    public class MapView : ContentPage
    {
        /*
         * Generic Mapview function for both Android and IOS
         * Shown on MainPage when App is started
        */

        SearchBar searchBar;

        public MapView()
        {
            Title = "Map view";

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
            pin.Clicked += this.OnLabelClick;


            searchBar = new SearchBar { Placeholder = "Search", BackgroundColor = Xamarin.Forms.Color.White };
            searchBar.SearchButtonPressed += this.OnSearchClick;

            var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
                    ZXing.BarcodeFormat.QR_CODE
            };

            var scanner = new QRScan(options);
            scanner.OnScanResult += async (result) => {
                scanner.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () => {
                    scanner.readout = result.Text;
                    await Navigation.PopAsync();
                });
            };

            Button button = new Button() {Text = "QR"};
            //button.Clicked += this.OnQRClick;
            button.Clicked += async (sender, e) => {
                await Navigation.PushAsync(scanner);
            };




            var stack = new StackLayout { Spacing = 0 };
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(12, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            stack.Children.Add(searchBar);
            grid.Children.Add(map, 0, 0);
            Grid.SetRowSpan(map, 2);
            grid.Children.Add(button, 0, 1);
            //stack.Children.Add(map);
            stack.Children.Add(grid);
            Content = stack;


        }

        async void OnLabelClick(object sender, EventArgs e) {
            await Navigation.PushAsync(new StoryPage());
        } 

        async void OnQRClick(object sender, EventArgs e) {
            

        }

        public void OnSearchClick(object sender, EventArgs e) {
            if (searchBar.Text.Equals("Culc"))
            {
                Navigation.PushAsync(new StoryPage());
            }
        }
    }
}

