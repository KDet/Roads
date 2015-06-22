using Cirrious.MvvmCross.ViewModels;

namespace LvivRoads.Core.ViewModels
{
    public class HomeViewModel
        : BaseViewModel
    {
        public IMvxCommand TrackLocationCommand
        {
            get { return new MvxCommand(() => ShowViewModel<LocationViewModel>()); }
        }

        public IMvxCommand PayWayBetweenLocationsCommand
        {
            get { return new MvxCommand(() => ShowViewModel<TransitViewModel>()); }
        }

        public IMvxCommand PayWayFromLocationCommand
        {
            get { return new MvxCommand(() => ShowViewModel<CustomTransitViewModel>()); }
        }
    }
}