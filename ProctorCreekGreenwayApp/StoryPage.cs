using System;

using Xamarin.Forms;

namespace ProctorCreekGreenwayApp
{
    public class StoryPage : ContentPage
    {
        public StoryPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Image { Source = ImageSource.FromUri(new Uri("https://cdn.vox-cdn.com/thumbor/ZpNOKJWHr4EpxJ63KHHmFV82LPQ=/0x0:1500x1125/1200x800/filters:focal(630x443:870x683)/cdn.vox-cdn.com/uploads/chorus_image/image/56280919/Bridge_over_Chattahoochee.0.jpg")) },
                    new Label { Text = "Proctor Creek Greenway" },
                    new Label { Text = "This is the start of the Proctor Creek Greenway Path" }
                }
            };
        }
    }
}

