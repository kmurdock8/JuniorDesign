using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace ProctorCreekGreenwayApp
{
    public class QRScan : ZXingScannerPage
    {
        public string readout;

        public QRScan(ZXing.Mobile.MobileBarcodeScanningOptions options): base(options) {

        }
    }
}
