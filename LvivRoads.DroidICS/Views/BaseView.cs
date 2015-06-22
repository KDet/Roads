using Android.App;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.OS;
using Cirrious.MvvmCross.Droid.Fragging;
using LvivRoads.Core.Services;

namespace LvivRoads.DroidICS.Views
{
    public class BaseView : MvxFragmentActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContent();
            SetView();
        }
        protected virtual void SetContent() { }

        protected virtual void SetView()
        {
            int status = GooglePlayServicesUtil.IsGooglePlayServicesAvailable(BaseContext);
            if (status != ConnectionResult.Success)
            {
                const int requestCode = 10;
                Dialog dialog = GooglePlayServicesUtil.GetErrorDialog(status, this, requestCode);
                dialog.Show();
            }
            else
            {
                var mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);

                mapFragment.Map.UiSettings.MyLocationButtonEnabled = true;
                mapFragment.Map.UiSettings.CompassEnabled = true;
                mapFragment.Map.UiSettings.RotateGesturesEnabled = true;
                mapFragment.Map.UiSettings.ScrollGesturesEnabled = true;
                mapFragment.Map.UiSettings.TiltGesturesEnabled = true;
                mapFragment.Map.UiSettings.ZoomControlsEnabled = true;
                mapFragment.Map.UiSettings.ZoomGesturesEnabled = true;

                mapFragment.Map.MapType = (int)MapTypes.Roadmap;
                mapFragment.Map.MyLocationEnabled = true;
                mapFragment.Map.TrafficEnabled = true;
            }
        }
    }
}