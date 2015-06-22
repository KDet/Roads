using Cirrious.MvvmCross.Plugins.Location;
using LvivRoads.Core.Services.Direction;
using LvivRoads.Core.Services.Transit;

namespace LvivRoads.Core.ViewModels
{
    public class CustomTransitViewModel : TransitViewModel
    {
        public CustomTransitViewModel(IMvxLocationWatcher locationWatcher, IDirectionService directionService,
            ITransitService transitService) : base(locationWatcher, directionService, transitService) { }

        protected override DirectionRequest ComputeDirectionRequest()
        {
            var request = base.ComputeDirectionRequest();
            request.Origin = Location;
            return request;
        }

        public override void Start()
        {
            StartLocationWatchCommand.Execute();
        }
    }
}