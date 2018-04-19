using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace ProctorCreekGreenwayApp
{
    public class StoryPage : ContentPage
    {
        public static List<int> imageIDs;
        public static Story story;

        public StoryPage(Story s)
        {
            // Initialize global variables
            story = s;
            imageIDs = new List<int>();

            List<string> images = s.Images;
            // Get the IDs of each image
            foreach (string iurl in images) {
                string[] split = iurl.Split('/');
                int imageID = -1;
                try {
                    imageID = System.Convert.ToInt32(split[4]);
                    imageIDs.Add(imageID);
                } catch (Exception e) {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        /*
         * Populate story page with images from database and story info
         */
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Create page layout with the story name as the title
            var stack = new StackLayout
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Label title = new Label
            {
                Text = story.Name,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
            };
            stack.Children.Add(title);

            // Get image URLs and add each image to stack layout
            foreach (int id in imageIDs) {
                string iurl = await App.DBManager.GetImageURLAsync(id);
                var img = new Image { Source = new Uri(iurl) };
                stack.Children.Add(img);
            }

            // Add body of the story page
            Label body = new Label { Text = story.Details };
            stack.Children.Add(body);

            Content = new ScrollView() { Content = stack };
        }
    }
}

