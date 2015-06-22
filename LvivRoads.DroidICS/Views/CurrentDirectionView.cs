using System.Linq;
using Android.App;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Cirrious.MvvmCross.Droid.Fragging;
using LvivRoads.Core.Services;
using LvivRoads.Core.Services.Internal;
using LvivRoads.Core.ViewModels;


namespace LvivRoads.DroidICS.Views
{
    [Activity(Label = "View for DirectionView")]
    public class CurrentDirectionView : DirectionView
    {
        
        protected override void SetView()
        {
            SetContentView(Resource.Layout.CurrentDirectionView);
        } 
    }
}