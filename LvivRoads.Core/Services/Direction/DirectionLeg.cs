using Newtonsoft.Json;

namespace LvivRoads.Core.Services.Direction
{
	[JsonObject(MemberSerialization.OptIn)]
	public class DirectionLeg
	{
        /// <summary>
        /// contains an array of steps denoting information about each separate step of the leg of the journey
        /// </summary>
		[JsonProperty("steps")]
		public DirectionStep[] Steps { get; set; }

        /// <summary>
        /// indicates the total distance covered by this leg
        /// </summary>
        [JsonProperty("distance")]
        public ValueText Distance { get; set; }

        /// <summary>
        ///  indicates the total duration of this leg
        /// </summary>
		[JsonProperty("duration")]
		public ValueText Duration { get; set; }

        /// <summary>
        /// duration_in_traffic indicates the total duration of this leg, taking into account current traffic conditions. The duration in traffic will only be returned if all of the following are true:
        /// - The directions request includes a departure_time parameter set to a value within a few minutes of the current time.
        /// - The request includes a valid Maps for Business client and signature parameter.
        /// - Traffic conditions are available for the requested route.
        /// - The directions request does not include stopover waypoints.
        /// </summary>
        [JsonProperty("duration_in_traffic")]
        public ValueText DurationInTraffic { get; set; }

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
        /// contains the latitude/longitude coordinates of the origin of this leg. Because the Directions API calculates directions between locations by using the nearest transportation option (usually a road) at the start and end points, start_location may be different than the provided origin of this leg if, for example, a road is not near the origin.
        /// </summary>
		[JsonProperty("start_location")]
		public LatitudeLongitude StartLocation { get; set; }

        /// <summary>
        /// contains the latitude/longitude coordinates of the given destination of this leg. Because the Directions API calculates directions between locations by using the nearest transportation option (usually a road) at the start and end points, end_location may be different than the provided destination of this leg if, for example, a road is not near the destination.
        /// </summary>
		[JsonProperty("end_location")]
		public LatitudeLongitude EndLocation { get; set; }

        /// <summary>
        /// contains the human-readable address (typically a street address) reflecting the start_location of this leg.
        /// </summary>
		[JsonProperty("start_address")]
		public string StartAddress { get; set; }

        /// <summary>
        /// contains the human-readable address (typically a street address) reflecting the end_location of this leg.
        /// </summary>
		[JsonProperty("end_address")]
		public string EndAddress { get; set; }
	}
}
