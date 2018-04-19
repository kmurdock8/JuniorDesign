using System;

using Xamarin.Forms;

namespace ProctorCreekGreenwayApp
{
    public class StoryPage : ContentPage
    {
        public StoryPage(Story s)
        {
            Content = new StackLayout
            {
                Children = {
                    new Label {Text = s.Name},
                    new Label {Text = s.Details},
                }
            };
        }
    }
}

