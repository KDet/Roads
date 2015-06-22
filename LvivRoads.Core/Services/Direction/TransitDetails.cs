using Newtonsoft.Json;

namespace LvivRoads.Core.Services.Direction
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TransitDetails
    {
        /// <summary>
        /// arrival_stop contains information about the stop/station for this part of the trip.
        /// </summary>
        [JsonProperty("arrival_stop")]
        public StopStation ArrivalStop { get; set; }

        /// <summary>
        /// departure_stop contains information about the stop/station for this part of the trip.
        /// </summary>
        [JsonProperty("departure_stop")]
        public StopStation DepartureStop { get; set; }

        /// <summary>
        /// contains the estimated time of arrival for this leg. This property is only returned for transit directions. The result is returned as a Time
        /// </summary>
        [JsonProperty("arrival_time")]
        public Time ArrivalTime { get; set; }

        /// <summary>
        /// departure_time contains the estimated time of departure for this leg, specified as a Time object. The departure_time is only available for transit directions. 
        /// </summary>
        [JsonProperty("departure_time")]
        public Time DepartureTime { get; set; }

        /// <summary>
        /// headsign specifies the direction in which to travel on this line, as it is marked on the vehicle or at the departure stop. This will often be the terminus station.
        /// </summary>
        [JsonProperty("headsign")]
        public string Headsign { get; set; }

        /// <summary>
        /// headway specifies the expected number of seconds between departures from the same stop at this time. For example, with a headway value of 600, you would expect a ten minute wait if you should miss your bus.
        /// </summary>
        [JsonProperty("headway")]
        public int Headway { get; set; }

        /// <summary>
        /// num_stops contains the number of stops in this step, counting the arrival stop, but not the departure stop. For example, if your directions involve leaving from Stop A, passing through stops B and C, and arriving at stop D, num_stops will return 3.
        /// </summary>
        [JsonProperty("num_stops")]
        public int NumStops { get; set; }

        /// <summary>
        /// line contains information about the transit line used in this step
        /// </summary>
        [JsonProperty("line")]
        public  TransitLine Line { get; set; }
    }
}