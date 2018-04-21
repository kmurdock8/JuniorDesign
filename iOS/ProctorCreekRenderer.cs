using System;
using System.Diagnostics;
using System.Collections.Generic;
using Foundation;
using MapKit;
using ProctorCreekGreenwayApp;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;

[assembly: ExportRenderer(typeof(ProctorCreekMap), typeof(CustomMapRenderer))]
namespace ProctorCreekGreenwayApp
{

    public class CustomMapRenderer : MapRenderer
    {
        IList<Pin> pins;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView; //grabs MapView from iOS
                if (nativeMap != null)
                {
                    nativeMap.RemoveAnnotations(nativeMap.Annotations); //clears other annotations
                    nativeMap.GetViewForAnnotation = null;
                    //Remove event handlers here if needed.
                }
            }
            if (e.NewElement != null)
            {
                var formsMap = (ProctorCreekMap)e.NewElement; //grabs Map from Xamarin
                var nativeMap = Control as MKMapView;
                pins = formsMap.Pins; //sets pins equal to pins from Xamarin
                nativeMap.GetViewForAnnotation = this.GetViewForAnnotation; //sets handler for annotation
                //Add event handlers here if needed.
            }
        }

        //Builds MKAnnotationView for display.
        MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {

            MKAnnotationView annotationView = null;

            //If the annotation is the user's location, simply return null
            //if (annotation is MKUserLocation) return null;

            //Grab pin that matches location
            ProctorCreekPin pin = GetPin(annotation as MKPointAnnotation);

            //If pin is null, we fucked up
            if (pin == null) throw new Exception("Pin not found.");

            annotationView = mapView.DequeueReusableAnnotation(pin.Id.ToString());

            if (annotationView == null)
            {

                annotationView = new MKPinAnnotationView(annotation, pin.story.ID.ToString());

                //Create container UIStackView.
                UIStackView containerStackView = new UIStackView();
                containerStackView.Axis = UILayoutConstraintAxis.Horizontal;
                containerStackView.Distribution = UIStackViewDistribution.FillEqually; //Play around with this
                containerStackView.Alignment = UIStackViewAlignment.Fill;
                containerStackView.Spacing = (System.nfloat)16.0;

                //Create new UIStackView for the left side, and set it up.
                UIStackView stackViewLeft = new UIStackView();
                stackViewLeft.Axis = UILayoutConstraintAxis.Vertical;
                stackViewLeft.Distribution = UIStackViewDistribution.Fill;
                stackViewLeft.Alignment = UIStackViewAlignment.Center;
                stackViewLeft.Spacing = (System.nfloat)16.0;

                //Create new UIStackView for the right side, and set it up.
                UIStackView stackViewRight = new UIStackView();
                stackViewRight.Axis = UILayoutConstraintAxis.Vertical;
                stackViewRight.Distribution = UIStackViewDistribution.EqualSpacing;
                stackViewRight.Alignment = UIStackViewAlignment.Center;
                stackViewRight.Spacing = (System.nfloat)16.0;

                //Create a new Label for the left side, and set the storyname.
                UILabel storyNameLabel = new UILabel();
                storyNameLabel.Text = pin.story.Name;
                storyNameLabel.AdjustsFontSizeToFitWidth = true;
                //Create a new TextView for the right side, and set it to DescriptionShort
                //from the pin. Also set to not editable.
                UILabel shortDescText = new PCGUILabel();
                shortDescText.Text = pin.story.Details;
                shortDescText.Lines = 12;
                shortDescText.UserInteractionEnabled = true;

                ((PCGUILabel)shortDescText).pin = pin;
                UITapGestureRecognizer descTextTap = new UITapGestureRecognizer(tap => this.OnShortDescClick(tap))
                {
                    NumberOfTapsRequired = 1
                };
                shortDescText.AddGestureRecognizer(descTextTap);

                //Create a new Label that says "Continue Reading..." for the right side.
                UILabel continueReadingLabel = new UILabel();
                continueReadingLabel.Text = "Continue Reading";

                //Create a new image for the left side, and set it from Story.PictureURL

                NSData imageData = null;
                imageData = NSData.FromUrl(new NSUrl(pin.ImageURL));

                UIImage image = null;

                //Check if imageData is null. If it's not, then make an image
                if (imageData != null)
                {
                    image = new UIImage(imageData);
                    System.Diagnostics.Debug.WriteLine(image.Size);
                }


                //Add the label (for the left side) to stackViewLeft.
                stackViewLeft.AddArrangedSubview(storyNameLabel);

                //If the image isn't null, then add a UIImageView to stackView.
                if (image != null)
                {
                    UIImageView imageView = new ScaledImageView(image);
                    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                    stackViewLeft.AddArrangedSubview(imageView);
                    stackViewLeft.AddConstraint(NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1.0f, 200));
                }

                //Add the labels (for the right side) to stackViewRight.
                stackViewRight.AddArrangedSubview(shortDescText);

                //stackViewRight.AddConstraint(topR);
                //stackViewRight.AddConstraint(sidesR);


                /*var margins = stackViewRight.LayoutMarginsGuide;
                shortDescText.LeadingAnchor.Constraint(equalTo: margins.LeadingAnchor, constant: 20).isActive = true;*/
                //stackViewRight.AddArrangedSubview(continueReadingLabel);

                //Add the left and right StackViews to the container StackView.
                containerStackView.AddArrangedSubview(stackViewLeft);
                containerStackView.AddArrangedSubview(stackViewRight);

                /*var width = NSLayoutConstraint.Create(containerStackView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1.0f, 1.0f);
                var height = NSLayoutConstraint.Create(containerStackView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1.0f, 1.0f);
                containerStackView.AddConstraint(width);
                containerStackView.AddConstraint(height);*/
                annotationView.DetailCalloutAccessoryView = containerStackView;

            }


            annotationView.CanShowCallout = true;
            return annotationView;
        }

        //Finds matching pin. 
        ProctorCreekPin GetPin(MKPointAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (ProctorCreekPin p in this.pins)
            {
                if (p.Position == position)
                {
                    return p;
                }
            }
            return null;
        }

        CGSize onScreenPointSizeOfImageInImageView(UIImageView imageView)
        {
            nfloat scale;
            if (imageView.Frame.Size.Width > imageView.Frame.Size.Height)
            {
                if (imageView.Image.Size.Width > imageView.Image.Size.Height)
                {
                    scale = imageView.Image.Size.Height / imageView.Frame.Size.Height;
                }
                else
                {
                    scale = imageView.Image.Size.Width / imageView.Frame.Size.Width;
                }
            }
            else
            {
                if (imageView.Image.Size.Width > imageView.Image.Size.Height)
                {
                    scale = imageView.Image.Size.Width / imageView.Frame.Size.Width;
                }
                else
                {
                    scale = imageView.Image.Size.Height / imageView.Frame.Size.Height;
                }
            }
            return new CGSize(imageView.Image.Size.Width / scale, imageView.Image.Size.Height / scale);

        }

        void OnShortDescClick(UIGestureRecognizer e) {
            var myView = e.View as PCGUILabel;
            MessagingCenter.Send<ProctorCreekMap, ProctorCreekPin>(new ProctorCreekMap(), "Navigation", myView.pin);
        }

        class ScaledImageView : UIImageView {

            public ScaledImageView(UIImage i) : base(i) { }

            public override CGSize IntrinsicContentSize
            {
                get
                {
                    var myImage = this.Image;
                    var myImageWidth = myImage.Size.Width;
                    var myImageHeight = myImage.Size.Height;
                    var myViewWidth = this.Frame.Size.Width;
                    System.Diagnostics.Debug.Print("Custom UIImageView called");
                    var ratio = myViewWidth / myImageWidth;
                    var scaledHeight = myImageHeight * ratio;

                    return new CGSize(myViewWidth, scaledHeight);
                }
            }
        }

        class PCGUILabel: UILabel {
            public ProctorCreekPin pin { get; set; }
        }



    }



}