using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProctorCreekGreenwayApp
{
    public class RestService
    {
        /* An HTTP client to retreive info sent from web API */
        HttpClient client;

        /* The base URL of the web API */
        private const string apiURL = "https://tranquil-wildwood-19722.herokuapp.com/api/v1";

        /*
         * Simple getter and setter method to populate the list of stories
         */
        public List<Story> Stories { get; private set; }

        public List<string> Images { get; private set; }

        /*
         * Constructor initializes an HTTP client to access central DB
         */
        public RestService()
        {
            client = new HttpClient(); 
        }

        /*
         * Gets the list of all the stories stored in the central DB
         */
        public async Task<List<Story>> GetStoriesAsync() {
            // Initialize stories list
            Stories = new List<Story>();

            // Edit API URL to retreive all stories
            string storiesURL = apiURL + "/story";
            Uri storiesUri = new Uri(string.Format(storiesURL, string.Empty));

            try {
                HttpResponseMessage response = await client.GetAsync(storiesUri);

                // Check if request was successful
                if (response.IsSuccessStatusCode) {
                    // Get content of response and translate it to a list of stories
                    var content = await response.Content.ReadAsStringAsync();
                    JToken token = JObject.Parse(content);
                    var meta = token.SelectToken("meta");
                    JArray storyArr = (JArray)token.SelectToken("objects");
                    Stories = storyArr.ToObject<List<Story>>();
                } else {
                    Debug.WriteLine(response);
                }
            } catch (Exception e) {
                Debug.WriteLine("EXCEPTION THROWN:");
                Debug.WriteLine(e.Message);

            }

            return Stories;
        }

        public async Task<string> GetImageURLAsync(int imageID) 
        {
            // Edit api URL to retreive this image info from database
            string imageURL = apiURL + "/storyimage/" + imageID;
            Uri imageUri = new Uri(string.Format(imageURL, string.Empty));

            // URL to return; will hold the URL to actual image
            string iurl = "";

            try {
                HttpResponseMessage response = await client.GetAsync(imageUri);

                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Get content of response and translate it to this stories image list
                    var content = await response.Content.ReadAsStringAsync();
                    JToken token = JObject.Parse(content);
                    iurl = (string)token.SelectToken("image");
                }
                else
                {
                    Debug.WriteLine(response);
                }
            } catch (Exception e) {
                Debug.WriteLine("EXCEPTION THROWN:");
                Debug.WriteLine(e.Message);
            }

            return iurl;
        }


    } // end of restservice class
}
