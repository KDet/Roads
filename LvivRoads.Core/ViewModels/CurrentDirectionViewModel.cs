using Cirrious.MvvmCross.Plugins.Location;
using LvivRoads.Core.Services.Direction;

namespace LvivRoads.Core.ViewModels
{
    public class CurrentDirectionViewModel : DirectionViewModel
    {
        public CurrentDirectionViewModel(IMvxLocationWatcher locationWatcher, IDirectionService directionService) : base(locationWatcher, directionService) { }

        protected override DirectionRequest ComputeDirectionRequest()
        {
            var request = base.ComputeDirectionRequest();
            request.Origin = Location;
            return request;
        }
    }
}