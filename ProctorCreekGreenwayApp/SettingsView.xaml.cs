using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProctorCreekGreenwayApp
{
    public partial class SettingsView : ContentPage
    {
        private TapGestureRecognizer lblTap; /* Tracks if someone taps the location services label */

        public SettingsView()
        {
            InitializeComponent();

            // TODO: Notification preference is always set on; update this to depend on user preference
            notifications.OnChanged += this.OnNotificationsChanged;
            selectAll.OnChanged += this.OnSelectAllChanged;
            // TODO: implement event handlers for each interest that updates DB

            //
            locationLbl.Text = "To turn location services on/off, "
                    + "click here to go to your device settings.";
            lblTap = new TapGestureRecognizer();
            // TODO: implement below function later
            //lblTap.Tapped += OnLocationLblTapped;
        }

        private void OnNotificationsChanged(object sender, EventArgs e)
        {
            // User changed whether or not they want to recieve notifications
            // TODO: after adding DB, update DB to reflect this choice
            if (notifications.On) {
                // User changed notifications on
                music.IsEnabled = true;
                history.IsEnabled = true;
                food.IsEnabled = true;
                art.IsEnabled = true;
                nature.IsEnabled = true;
                architecture.IsEnabled = true;
                selectAll.IsEnabled = true;
            } else {
                // Turned notifications off; Grey out interest list
                music.On = false;
                music.IsEnabled = false;
                history.On = false;
                history.IsEnabled = false;
                food.On = false;
                food.IsEnabled = false;
                art.On = false;
                art.IsEnabled = false;
                nature.On = false;
                nature.IsEnabled = false;
                architecture.On = false;
                architecture.IsEnabled = false;
                selectAll.On = false;
                selectAll.IsEnabled = false;
            }
        }

        private void OnSelectAllChanged(object sender, EventArgs e) 
        {
            // TODO: make sure this also updates DB
            if (!selectAll.On) {
                // Unselect everything
                music.On = false;
                history.On = false;
                food.On = false;
                art.On = false;
                nature.On = false;
                architecture.On = false;
            } else {
                // Select everything
                music.On = true;
                history.On = true;
                food.On = true;
                art.On = true;
                nature.On = true;
                architecture.On = true;
            }
        }
          
        //async void OnLocationLblTapped(object sender, EventArgs e) {
            //if (Device.RuntimePlatform == Device.Android) {
               
            //}
       //}
    }
}
