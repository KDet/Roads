/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace LvivRoads.Core.Services.DistanceMatrix
{
	/// <summary>
	/// Provides a request for the Google Distance Matrix web service.
	/// </summary>
	public class DistanceMatrixRequest
	{
		/// <summary>
		/// (optional) Specifies what mode of transport to use when calculating directions.
		/// </summary>
		public TravelMode Mode { get; set; }

		/// <summary>
		/// (optional) Directions may be calculated that adhere to certain restrictions.
		/// </summary>
		public Avoid Avoid { get; set; }

		/// <summary>
		///  (optional) Specifies the unit system to use when expressing distance as text.
		///   <see cref="http://code.google.com/intl/it-IT/apis/maps/documentation/distancematrix/#unit_systems"/>
		/// </summary>
		public Units Units { get; set; }

		/// <summary>
		/// (optional) The language in which to return results.
		/// <see cref="http://code.google.com/apis/maps/faq.html#languagesupport" />
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		///  
		///  
		/// </summary>
		public bool? Sensor { get; set; }

		/// <summary>
		///  List of origin waypoints
		/// </summary>
		private SortedDictionary<int, Waypoint> _waypointsOrigin;

		/// <summary>
		/// List of destination waypoints
		/// </summary>
        private SortedDictionary<int, Waypoint> _waypointsDestination;

	    /// <summary>
	    /// Accessor method
	    /// </summary>
	    public SortedDictionary<int, Waypoint> WaypointsOrigin
	    {
	        get { return _waypointsOrigin ?? (_waypointsOrigin = new SortedDictionary<int, Waypoint>()); }
	        set { _waypointsOrigin = value; }
	    }

	    /// <summary>
	    /// Accessor method
	    /// </summary>
	    public SortedDictionary<int, Waypoint> WaypointsDestination
	    {
	        get { return _waypointsDestination ?? (_waypointsDestination = new SortedDictionary<int, Waypoint>()); }
	        set { _waypointsDestination = value; }
	    }

	    /// <summary>
		/// 
		/// </summary>
		/// <param name="destination"></param>
		public void AddOrigin(Waypoint destination)
		{
			WaypointsOrigin.Add(WaypointsOrigin.Count, destination);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="destination"></param>
		public void AddDestination(Waypoint destination)
		{
			WaypointsDestination.Add(WaypointsDestination.Count, destination);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		internal string WaypointsToUri(SortedDictionary<int, Waypoint> Waypoints)
		{
			if (Waypoints.Count == 0) return string.Empty;

			var sb = new StringBuilder();
		    foreach (Waypoint waypoint in Waypoints.Values)
		        sb.AppendFormat("{0}|", waypoint.ToString());
		    sb = sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}//end method

		/// <summary>
		/// Create URI for quering
		/// </summary>
		/// <returns></returns>
		internal Uri ToUri()
		{
			EnsureSensor(true);

			var qsb = new Internal.QueryStringBuilder()
				.Append("origins", WaypointsToUri(_waypointsOrigin))
				.Append("destinations", WaypointsToUri(WaypointsDestination))
				.Append("mode", Mode.ToString())
				.Append("language", Language)
				.Append("units", Units.ToString())
				.Append("sensor", (Sensor != null && Sensor.Value ? "true" : "false"))
				.Append("avoid", Avoid.ToString());

			var url = "json?" + qsb;

			return new Uri(url, UriKind.Relative);
		}

		private void EnsureSensor(bool throwIfNotSet)
		{
		    if (Sensor == null && throwIfNotSet) 
                throw new InvalidOperationException("Sensor isn't set to a valid value.");
		}
	}

}
