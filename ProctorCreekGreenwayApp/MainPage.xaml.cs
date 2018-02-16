using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProctorCreekGreenwayApp
{
    public partial class MainPage : MasterDetailPage
    {

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
