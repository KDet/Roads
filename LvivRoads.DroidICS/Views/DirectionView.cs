using System.Linq;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using LvivRoads.Core.Services;
using LvivRoads.Core.Services.Internal;
using LvivRoads.Core.ViewModels;


namespace LvivRoads.DroidICS.Views
{
    [Activity(Label = "View for DirectionView")]
    public class DirectionView : LocationView
    {
        protected override void SetContent()
        {
            SetContentView(Resource.Layout.DirectionView);
        }

        protected override void SetView()
        {
            base.SetView();
            var viewModel = (DirectionViewModel)ViewModel;

            var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
            var googleMap = mapFragment.Map;

            viewModel.DirectionRoutesUpdated += (sender, args) =>
            {

                var routs = args.Value;
                var firstRout = routs.FirstOrDefault();
                if (firstRout != null)
                {
                    googleMap.Clear();
                    var lineOptions = new PolylineOptions();
                    var points = PolylineEncoder.Decode(firstRout.OverviewPolyline.Points)
                        .Select(point => new LatLng(point.Latitude, point.Longitude))
                        .ToArray();
                    lineOptions.Add(points);
                    //lineOptions.InvokeWidth(5);
                    lineOptions.InvokeColor(Color.BlueViolet);
                    lineOptions.Geodesic(true);
                    googleMap.AddPolyline(lineOptions);

                    foreach (var leg in firstRout.Legs)
                        foreach (var step in leg.Steps)
                        {
                            var options = new MarkerOptions();
                            options.SetPosition(new LatLng(step.StartLocation.Latitude, step.StartLocation.Longitude));
                            options.SetTitle(ConvertUtil.HtmlToPlainText(step.HtmlInstructions));
                            if (step.TransitDetails != null && step.TransitDetails.Line != null &&
                                step.TransitDetails.Line.Vehicle != null)
                            {
                                options.SetSnippet(step.TransitDetails.Line.Vehicle.Name + ": " +
                                                   step.TransitDetails.Headway / 60.0 + "хв.");
                                //var img = 
                                //  //  await 
                                //    BitmapFactory.DecodeStream(
                                //            new URL("http:" + step.TransitDetails.Line.Vehicle.Icon).OpenConnection().InputStream);
                                //options.InvokeIcon(BitmapDescriptorFactory.FromBitmap(img));
                            }
                            else
                                options.SetSnippet(step.TravelMode.ToString());
                            options.InvokeIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow));
                            googleMap.AddMarker(options);
                        }
                }
            };
        }        
    }
}