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
                    new Label { Text = "Clough Undergraduate Learning Commons" },
                    new Label { Text = "Did you know that The Internship was filmed here?" }
                }
            };
        }
    }
}

