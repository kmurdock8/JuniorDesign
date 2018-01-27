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
                    new Image { Source = ImageSource.FromUri(new Uri("https://c3.staticflickr.com/7/6137/6013225441_7bed8709e8_b.jpg")) },
                    new Label { Text = "Clough Undergraduate Learning Commons" },
                    new Label { Text = "Did you know that The Internship was filmed here?" }
                }
            };
        }
    }
}

