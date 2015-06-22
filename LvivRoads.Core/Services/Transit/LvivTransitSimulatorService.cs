using System;
using System.Collections.Generic;
using System.Linq;
using LvivRoads.Core.Services.Direction;

namespace LvivRoads.Core.Services.Transit
{
    public class LvivTransitSimulatorService : ITransitService
    {
        public List<LatitudeLongitude> GetTransportPosition(LatitudeLongitude myLocation, double radius)
        {
            var simulationBuses = (int) (radius*100);
            var random = new Random(DateTime.UtcNow.Millisecond);
            var transports = new List<LatitudeLongitude>();
            for (int i = 0; i < simulationBuses; i++)
                transports.Add(new LatitudeLongitude(
                    myLocation.Latitude + (random.NextDouble() - random.NextDouble())*radius,
                    myLocation.Longitude + (random.NextDouble() - random.NextDouble())*radius));
            return transports;
        }

        public LatitudeLongitude[] GetTransportPosition(DirectionStep directionStep)
        {
            return GetTransportPositionEnumerable(directionStep).ToArray();
        }

        private static IEnumerable<LatitudeLongitude> GetTransportPositionEnumerable(DirectionStep directionStep)
        {
            var roadPoints = directionStep.Polyline != null && directionStep.TravelMode == TravelMode.Transit
                ? PolylineEncoder.Decode(directionStep.Polyline.Points).ToArray()
                : new LatitudeLongitude[0];
            var random = new Random();
            var simulationVehicleNumber = roadPoints.Length / 3;
            if (simulationVehicleNumber < 3 && simulationVehicleNumber > 0)
                yield return roadPoints[0];
            else
                for (int i = 0; i < simulationVehicleNumber - 3; i++)
                    yield return roadPoints[i + random.Next(0, 3)];
        }
    }
}
