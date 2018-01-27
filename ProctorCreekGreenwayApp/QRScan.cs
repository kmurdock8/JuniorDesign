using System;
using Xamarin.Forms;
namespace ProctorCreekGreenwayApp
{
    public class QRScan : ContentPage
    {
        public QRScan()
        {
            Content = new StackLayout()
            {
                Children = {
                    new Label() {Text = "QR Scanner Here"}
                }
            };
        }
    }
}
