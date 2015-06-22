using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using LvivRoads.Core.ViewModels;

namespace LvivRoads.DroidICS.Views
{
    [Activity(Label = "View for LocationViewModel")]
    public class LocationView : BaseView
    {
        protected override void SetContent()
        {
            SetContentView(Resource.Layout.LocationView);
        }

        protected override void SetView()
        {
            base.SetView();
            var viewModel = (LocationViewModel) ViewModel;

            var mapFragment = (SupportMapFragment) SupportFragmentManager.FindFragmentById(Resource.Id.map);
            var googleMap = mapFragment.Map;

            mapFragment.Map.MyLocationButtonClick += (sender, args) => viewModel.StartLocationWatchCommand.Execute();
            viewModel.LocationUpdated += (sender, args) =>
            {
                googleMap.MoveCamera(CameraUpdateFactory.NewLatLng(new LatLng(args.Value.Latitude, args.Value.Longitude)));
                googleMap.AnimateCamera(CameraUpdateFactory.ZoomTo(15));
            };
        }
    }
}