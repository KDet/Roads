using System.Collections.Concurrent;
using System.Collections.Generic;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using LvivRoads.Core.Services.Direction;
using LvivRoads.Core.ViewModels;


namespace LvivRoads.DroidICS.Views
{
    [Activity(Label = "View for TransitView")]
    public class TransitView : DirectionView
    {
        protected override void SetContent()
        {
            SetContentView(Resource.Layout.TransitView);
        }

        protected override void SetView()
        {
            base.SetView();

            var viewModel = (TransitViewModel)ViewModel;

            var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
            var googleMap = mapFragment.Map;

            var points = new ConcurrentDictionary<DirectionStep, List<Marker>>();
            viewModel.TransitPositionUpdated += (sender, args) =>
            {
                
                List<Marker> value;
                if (points.TryGetValue(args.DirectionStep, out value))
                    for (int i = value.Count - 1; i >= 0; i--)
                        value[i].Remove();

                var markers = new List<Marker>();
                foreach (var point in args.Points)
                {
                    var option = new MarkerOptions();
                    option.SetPosition(new LatLng(point.Latitude, point.Longitude));
                    var marker = googleMap.AddMarker(option);
                    markers.Add(marker);
                }
                points[args.DirectionStep] = markers;
            };
        }
    }
}