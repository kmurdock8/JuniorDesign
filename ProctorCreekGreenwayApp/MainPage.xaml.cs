using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProctorCreekGreenwayApp
{
    public partial class MainPage : MasterDetailPage
    {
        /**
         * Main page uses a MasterDetailPage made up of the map view
         * and settings pane.
         */

        public MainPage()
        {
            Master = new SettingsView();
            Detail = new NavigationPage(new MapView());
            IsGestureEnabled = true;
            IsPresented = false;

            InitializeComponent();
        }
    }
}
