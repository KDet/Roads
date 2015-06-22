using Android.App;

namespace LvivRoads.DroidICS.Views
{
    [Activity(Label = "View for CustomTransitView")]
    public class CustomTransitView : TransitView
    {
        protected override void SetContent()
        {
            SetContentView(Resource.Layout.CustomTransitView);
        }
    }
}