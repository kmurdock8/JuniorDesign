using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProctorCreekGreenwayApp
{
    public partial class SettingsView : ContentPage
    {
        private TapGestureRecognizer lblTap; /* Tracks if someone taps the location services label */
        User currentUser;
        int userID;                       

        public SettingsView()
        {
            InitializeComponent();

            // Make sure user is in DB
            currentUser = App.Database.GetUser().Result;
            if (currentUser == null) {
                // Create user in DB
                userID = App.Database.AddUser().Result;
                currentUser = App.Database.GetUser().Result;
            } else {
                userID = currentUser.ID;
            }

            // Initialize UI based on user's stored preferences
            if (currentUser.Notifications) {
                notifications.On = true;
            } else {
                notifications.On = false;
                music.IsEnabled = false;
                history.IsEnabled = false;
                food.IsEnabled = false;
                art.IsEnabled = false;
                nature.IsEnabled = false;
                architecture.IsEnabled = false;
            }
            if (currentUser.MusicNotifs) {
                music.On = true;
            } else {
                music.On = false;
            }
            if (currentUser.HistoryNotifs)
            {
                history.On = true;
            }
            else
            {
                history.On = false;
            }
            if (currentUser.FoodNotifs)
            {
                food.On = true;
            }
            else
            {
                food.On = false;
            }
            if (currentUser.ArtNotifs)
            {
                art.On = true;
            }
            else
            {
                art.On = false;
            }
            if (currentUser.NatureNotifs)
            {
                nature.On = true;
            }
            else
            {
                nature.On = false;
            }

            if (currentUser.ArchitectureNotifs)
            {
                architecture.On = true;
            }
            else
            {
                architecture.On = false;
            }
            if (music.On && history.On && food.On && art.On && nature.On && architecture.On) {
                selectAll.On = true;
            }
 

            // Initialize event handlers
            notifications.OnChanged += this.OnNotificationsChanged;
            selectAll.OnChanged += this.OnSelectAllChanged;
            music.OnChanged += this.OnMusicChanged;
            history.OnChanged += this.OnHistoryChanged;
            food.OnChanged += this.OnFoodChanged;
            art.OnChanged += this.OnArtChanged;
            nature.OnChanged += this.OnNatureChanged;
            architecture.OnChanged += this.OnArchitectureChanged;


            locationLbl.Text = "To turn location services on/off, "
                    + "click here to go to your device settings.";
            
            lblTap = new TapGestureRecognizer();
            // TODO: implement below function later (sprint 3)
            //lblTap.Tapped += OnLocationLblTapped;
        }

        private void OnNotificationsChanged(object sender, EventArgs e)
        {
            // User changed whether or not they want to recieve notifications
            // TODO: after adding DB, update DB to reflect this choice
            int success = 0;
            if (notifications.On) {
                // User changed notifications on
                success = App.Database.UpdateNotifications(currentUser, true).Result;
                music.IsEnabled = true;
                history.IsEnabled = true;
                food.IsEnabled = true;
                art.IsEnabled = true;
                nature.IsEnabled = true;
                architecture.IsEnabled = true;
                selectAll.IsEnabled = true;
            } else {
                // Turned notifications off; Grey out interest list
                success = App.Database.UpdateNotifications(currentUser, false).Result;
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

        private void OnMusicChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (music.On) {
                result = App.Database.UpdateMusicNotifs(currentUser, true).Result;
            } else {
                result = App.Database.UpdateMusicNotifs(currentUser, false).Result;
            }
        }

        private void OnHistoryChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (history.On)
            {
                result = App.Database.UpdateHistoryNotifs(currentUser, true).Result;
            }
            else
            {
                result = App.Database.UpdateHistoryNotifs(currentUser, false).Result;
            }
        }

        private void OnFoodChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (food.On)
            {
                result = App.Database.UpdateFoodNotifs(currentUser, true).Result;
            }
            else
            {
                result = App.Database.UpdateFoodNotifs(currentUser, false).Result;
            }
        }

        private void OnArtChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (art.On)
            {
                result = App.Database.UpdateArtNotifs(currentUser, true).Result;
            }
            else
            {
                result = App.Database.UpdateArtNotifs(currentUser, false).Result;
            }
        }

        private void OnNatureChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (nature.On)
            {
                result = App.Database.UpdateNatureNotifs(currentUser, true).Result;
            }
            else
            {
                result = App.Database.UpdateNatureNotifs(currentUser, false).Result;
            }
        }

        private void OnArchitectureChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (architecture.On)
            {
                result = App.Database.UpdateArchitectureNotifs(currentUser, true).Result;
            }
            else
            {
                result = App.Database.UpdateArchitectureNotifs(currentUser, false).Result;
            }
        }


        //async void OnLocationLblTapped(object sender, EventArgs e) {
        //if (Device.RuntimePlatform == Device.Android) {

        //}
        //}
    }
}
