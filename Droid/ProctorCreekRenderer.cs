using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using ProctorCreekGreenwayApp;
using ProctorCreekGreenwayApp.Droid;
using Android.Graphics;

[assembly: ExportRenderer(typeof(ProctorCreekMap), typeof(CustomMapRenderer))]
namespace ProctorCreekGreenwayApp
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        IList<Pin> pins;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (ProctorCreekMap)e.NewElement;
                pins = formsMap.Pins;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view = null;
                var pin = GetPin(marker);
                if (pin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                view = inflater.Inflate(Resource.Layout.MapInfoWindow, null); // Uses layout from resources
                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle); // creates a variable out of the info window title
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle); // creates a variable out of the info window subtitle

                if (infoTitle != null)
                {
                    infoTitle.Text = pin.story.Name;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = pin.story.Details;
                }

                return view;
            }
            return null;
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
           
        }

        ProctorCreekPin GetPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (ProctorCreekPin p in this.pins)
            {
                if (p.Position == position)
                {
                    return p;
                }
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
    }



}