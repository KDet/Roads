using System.Collections.Generic;
using LvivRoads.Core.Services.Direction;

namespace LvivRoads.Core.Services.Transit
{
    public interface ITransitService
    {
        LatitudeLongitude[] GetTransportPosition(DirectionStep directionRoute);
    }
}