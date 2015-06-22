using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using LvivRoads.Core.Extensions;
using LvivRoads.Core.Services.Internal;

namespace LvivRoads.Core.Services.Direction
{
	public class DirectionRequest
	{
		/// <summary>
		/// The <see cref="Position"/> from which you wish to calculate directions.
		/// </summary>
		public Position Origin { get; set; }
		/// <summary>
		/// The <see cref="Position"/> from which you wish to calculate directions.
		/// </summary>
        public Position Destination { get; set; }

		/// <summary>
		/// Specifies the mode of transport to use when calculating directions. Valid values are specified in <see cref="TravelMode"/>s. 
		/// </summary>
		//TODO: add transit TravelMode and add to summary: If you set the mode to "transit" you must also specify either a departure_time or an arrival_time.
		[DefaultValue(TravelMode.Driving)]
		public TravelMode Mode { get; set; }

		/// <summary>The region code, specified as a ccTLD ("top-level domain") two-character value. See also Region biasing.</summary>
		/// <see cref="http://code.google.com/apis/maps/documentation/directions/#RequestParameters"/>
		/// <seealso cref="https://developers.google.com/maps/documentation/directions/#RegionBiasing"/>
		public string Region { get; set; }

		/// <summary>The language in which to return results. See the list of supported domain languages. 
		/// Note that we often update supported languages so this list may not be exhaustive. 
		/// If language is not supplied, the service will attempt to use the native language of the domain from which the request is sent.</summary>
		/// <see cref="http://code.google.com/apis/maps/documentation/directions/#RequestParameters"/>
		public string Language { get; set; }

		/// <summary>
		///  Indicates whether or not the directions request comes from a device with a location sensor. This value must be either true or false.
		/// </summary>
		public bool? Sensor { get; set; }

        private List<Position> _waypoints;
        public IEnumerable<Position> Waypoints
		{
			get
			{
                return _waypoints ?? new List<Position>();
			}
		    set
			{
				if (value == null)
				{
					//clear our reference.
					_waypoints = null; 
                    return;
				}
				
				//see if reference passed is a List<Location> instance.
                var list = value as List<Position> ?? new List<Position>(value);
                _waypoints = list;
			}
		}

        /// <summary>
        /// alternatives — If set to true, specifies that the Directions service may provide more than one route alternative in the response. Note that providing route alternatives may increase the response time from the server.
        /// </summary>
	    public bool? Alternatives { get; set; }

        /// <summary>
        /// units — Specifies the unit system to use when displaying results.
        /// </summary>
        public Units Units { get; set; }

        /// <summary>
        /// departure_time specifies the desired time of departure as seconds since midnight, January 1, 1970 UTC. The departure time may be specified in two cases:
        /// - For Transit Directions: One of departure_time or arrival_time must be specified when requesting directions.
        /// - For Driving Directions: Maps for Business customers can specify the departure_time to receive trip duration considering current traffic conditions. The departure_time must be set to within a few minutes of the current time.
        /// </summary>
        public DateTime? DepartureTime { get; set; }

        /// <summary>
        /// arrival_time specifies the desired time of arrival for transit directions as seconds since midnight, January 1, 1970 UTC. One of departure_time or arrival_time must be specified when requesting transit directions.
        /// </summary>
        public DateTime? ArrivalTime { get; set; }
	    

	    /// <summary>
		/// Adds a waypoint to the current request.
		/// </summary>
		/// <remarks>Google's API specifies 8 maximum for non-business (free) consumers, and up to 23 for (registered) business customers</remarks>
		/// <param name="waypoint"></param>
        public void AddWaypoint(Position waypoint)
		{
			if (waypoint == null) return;
            if (_waypoints == null) _waypoints = new List<Position>();
			_waypoints.Add(waypoint);
		}

		internal string WaypointsToUri()
		{
			if (_waypoints == null || _waypoints.Count == 0) return null;

			var sb = new StringBuilder();

            foreach (Position waypoint in _waypoints)
			{
				if (sb.Length > 0) sb.Append("|");
				sb.Append(waypoint);
			}

			return sb.ToString();
		}
        internal Uri ToUri()
		{
			EnsureSensor();
            EnsureTransit();
            var qsb = new QueryStringBuilder()
                .Append("origin", (Origin == null ? null : Origin.GetAsUrlParameter()))
                .Append("destination", (Destination == null ? null : Destination.GetAsUrlParameter()))
                .Append("mode", (Mode != TravelMode.Driving ? Mode.ToString().ToLowerInvariant() : null))
                .Append("waypoints", WaypointsToUri())
                .Append("region", Region)
                .Append("language", Language)
                .Append("sensor", Sensor != null && Sensor.Value ? "true" : "false")
                .Append("alternatives", Alternatives != null ? Alternatives.Value ? "true" : "false" : null)
                .Append("units", Units.ToString().ToLowerInvariant())
                .Append("departure_time", Mode == TravelMode.Transit && DepartureTime != null ? DepartureTime.Value.ToUnixTimestamp().ToString() : null)
                .Append("arrival_time", Mode == TravelMode.Transit && ArrivalTime != null ? ArrivalTime.Value.ToUnixTimestamp().ToString() : null);

			var url = string.Format("json?{0}", qsb);

			return new Uri(url, UriKind.Relative);
		}

		private void EnsureSensor()
		{
			if (Sensor == null) throw new InvalidOperationException("Sensor property hasn't been set.");
		}
	    private void EnsureTransit()
	    {
	        if (Mode == TravelMode.Transit && (DepartureTime == null && ArrivalTime == null))
	            throw new InvalidOperationException(
	                "In Transit mode you must also specify either a DepartureTime or an ArrivalTime");
	    }
	}
}