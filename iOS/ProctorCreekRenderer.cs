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
                annotationView.CalloutOffset = new CGPoint(0, 0);

                //Create container UIStackView.
                UIStackView containerStackView = new UIStackView();
                containerStackView.Axis = UILayoutConstraintAxis.Horizontal;
                containerStackView.Distribution = UIStackViewDistribution.FillEqually; //Play around with this
                containerStackView.Alignment = UIStackViewAlignment.Center;
                containerStackView.Spacing = (System.nfloat)16.0;

                //Create new UIStackView for the left side, and set it up.
                UIStackView stackViewLeft = new UIStackView();
                stackViewLeft.Axis = UILayoutConstraintAxis.Vertical;
                stackViewLeft.Distribution = UIStackViewDistribution.EqualSpacing;
                stackViewLeft.Alignment = UIStackViewAlignment.Center;
                stackViewLeft.Spacing = (System.nfloat)16.0;

                System.Diagnostics.Debug.WriteLine("Created 1st StackView");

                //Create new UIStackView for the right side, and set it up.
                UIStackView stackViewRight = new UIStackView();
                stackViewRight.Axis = UILayoutConstraintAxis.Vertical;
                stackViewRight.Distribution = UIStackViewDistribution.EqualSpacing;
                stackViewRight.Alignment = UIStackViewAlignment.Center;
                stackViewRight.Spacing = (System.nfloat)16.0;

                System.Diagnostics.Debug.WriteLine("Created 2nd StackView");

                //Create a new Label for the left side, and set the storyname.
                UILabel storyNameLabel = new UILabel();
                storyNameLabel.Text = pin.story.Name;

                System.Diagnostics.Debug.WriteLine("Created StoryNameLabel");

                //Create a new TextView for the right side, and set it to DescriptionShort
                //from the pin. Also set to not editable.
                UILabel shortDescText = new UILabel();
                shortDescText.Text = pin.story.Details;
                shortDescText.Lines = 10;

                //shortDescText.Editable = false;



                System.Diagnostics.Debug.WriteLine(shortDescText.Text);

                //Create a new Label that says "Continue Reading..." for the right side.
                UILabel continueReadingLabel = new UILabel();
                continueReadingLabel.Text = "Continue Reading";

                System.Diagnostics.Debug.WriteLine("Created ContinueReadingLabel");

                //Create a new image for the left side, and set it from Story.PictureURL
                //NSData imageData = NSData.FromUrl(new NSUrl(pin.story.PictureURL));


                NSData imageData = null;
                imageData = NSData.FromUrl(new NSUrl(pin.story.ImageURL));
                /*
                if (pin.story.Images.Count > 0)
                {
                    System.Diagnostics.Debug.Write("Non null story: ");
                    System.Diagnostics.Debug.WriteLine(pin.story.Name);
                    System.Diagnostics.Debug.WriteLine(pin.story.Images[0]);
                   imageData = NSData.FromUrl(new NSUrl(pin.story.Images[0]));
                }
                */
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
                    UIImageView imageView = new UIImageView(image);
                    imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
                    stackViewLeft.AddArrangedSubview(imageView);
                    NSLayoutConstraint topL = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                                        storyNameLabel, NSLayoutAttribute.Bottom, 1.0f, 2.0f);
                    NSLayoutConstraint sidesL = NSLayoutConstraint.Create(imageView, NSLayoutAttribute.Width, NSLayoutRelation.Equal,
                                                                         stackViewLeft, NSLayoutAttribute.Width, 1.0f, 0.0f);
                    stackViewLeft.AddConstraint(topL);
                    stackViewLeft.AddConstraint(sidesL);
                }
                //annotationView.Image = null;
                //Add the labels (for the right side) to stackViewRight.
                stackViewRight.AddArrangedSubview(shortDescText);
                NSLayoutConstraint topR = NSLayoutConstraint.Create(shortDescText, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                                   stackViewRight, NSLayoutAttribute.Top, 1.0f, 2.0f);
                NSLayoutConstraint sidesR = NSLayoutConstraint.Create(shortDescText, NSLayoutAttribute.Width, NSLayoutRelation.Equal,
                                                                     stackViewRight, NSLayoutAttribute.Width, 1.0f, 0.0f);

                stackViewRight.AddConstraint(topR);
                stackViewRight.AddConstraint(sidesR);


                /*var margins = stackViewRight.LayoutMarginsGuide;
                shortDescText.LeadingAnchor.Constraint(equalTo: margins.LeadingAnchor, constant: 20).isActive = true;*/
                stackViewRight.AddArrangedSubview(continueReadingLabel);

                //Add the left and right StackViews to the container StackView.
                containerStackView.AddArrangedSubview(stackViewLeft);
                containerStackView.AddArrangedSubview(stackViewRight);

                NSLayoutConstraint topLSV = NSLayoutConstraint.Create(stackViewLeft, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                                      containerStackView, NSLayoutAttribute.Top, 1.0f, 2.0f);
                NSLayoutConstraint topLSNL = NSLayoutConstraint.Create(storyNameLabel, NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                                                    stackViewLeft, NSLayoutAttribute.Top, 1.0f, 2.0f);
                stackViewLeft.AddConstraint(topLSNL);
                //Assign stackViewLeft to the LeftCalloutAccessory of the annotation.
                annotationView.DetailCalloutAccessoryView = containerStackView;
                //annotationView.LeftCalloutAccessoryView = new UIImageView(image);
                //annotationView.RightCalloutAccessoryView = shortDescLabel;
                //Assign stackViewRight to the RightCalloutAccessory of the annotation.
                //annotationView.RightCalloutAccessoryView = stackViewRight;

            }

            System.Diagnostics.Debug.WriteLine(annotationView.LeftCalloutAccessoryView);
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

    }



}