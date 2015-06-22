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
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace LvivRoads.Core.Services
{
	[JsonObject(MemberSerialization.OptIn)]
	public class LatitudeLongitude : Position, IEquatable<LatitudeLongitude>
	{
		public LatitudeLongitude() { }
        
        /// <summary>
		/// Create a new latlng instance with the given latitude and longitude coordinates.
		/// </summary>
		/// <param name="latitude"></param>
		/// <param name="longitude"></param>
		public LatitudeLongitude(decimal latitude, decimal longitude)
		{
			_latitude = Convert.ToDouble(latitude);
			_longitude = Convert.ToDouble(longitude);
		}

		/// <summary>
		/// Create a new latlng instance with the given latitude and longitude coordinates.
		/// </summary>
		/// <param name="latitude"></param>
		/// <param name="longitude"></param>
		public LatitudeLongitude(double latitude, double longitude)
		{
			_latitude = latitude;
			_longitude = longitude;
		}
		/// <summary>
		/// Create a new latlng instance with the given latitude and longitude coordinates.
		/// </summary>
		/// <param name="latitude"></param>
		/// <param name="longitude"></param>
		public LatitudeLongitude(float latitude, float longitude)
		{
			_latitude = Convert.ToDouble(latitude);
			_longitude = Convert.ToDouble(longitude);
		}

		private readonly double _latitude;
		private readonly double _longitude;

		/// <summary>
		/// Gets or sets the latitude coordinate
		/// </summary>
		[JsonProperty("lat")]
		public double Latitude
		{
			get { return _latitude; }
		}

		/// <summary>
		/// Gets or sets the longitude coordinate
		/// </summary>
		[JsonProperty("lng")]
		public double Longitude
		{
			get { return _longitude; }
		}

		/// <summary>
		/// Gets the string representation of the latitude and longitude coordinates.  Default format is "N6" for 6 decimal precision.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return ToString("N6");
		}

		/// <summary>
		/// Gets the string representation of the latitude and longitude coordinates.  The format is applies to a System.Double, so any format applicable for System.Double will work.
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public string ToString(string format)
		{
			var sb = new StringBuilder(50); //default to 50 in the internal array.
			sb.Append(Latitude.ToString(format, CultureInfo.InvariantCulture));
			sb.Append(", ");
			sb.Append(Longitude.ToString(format, CultureInfo.InvariantCulture));
            return sb.ToString();
		}

		/// <summary>
		/// Gets the current instance as a URL encoded value.
		/// </summary>
		/// <returns></returns>
		public override string GetAsUrlParameter()
		{
			//we're not returning crazy characters so just return the string.  
			//prevents the comma from being converted to %2c, expanding the single character to three characters.
			return ToString("R");
		}

		#region Parse

		/// <summary>
		/// Parses a LatLng from a set of latitude/longitude coordinates
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static LatitudeLongitude Parse(string value)
		{
			if (value == null) throw new ArgumentNullException("value");

			try
			{
				string[] parts = value.Split(',');

				if (parts.Length != 2) throw new FormatException("Missing data for points.");

				double latitude = double.Parse(parts[0].Trim(), CultureInfo.InvariantCulture);
				double longitude = double.Parse(parts[1].Trim(), CultureInfo.InvariantCulture);

				var latLng = new LatitudeLongitude(latitude, longitude);

				return latLng;
			}
			catch (Exception ex)
			{
				throw new FormatException("Failed to parse LatLng.", ex);
			}
		}
		#endregion

		public override bool Equals(object obj)
		{
			return Equals(obj as LatitudeLongitude);
		}
		public bool Equals(LatitudeLongitude other)
		{
		    return other != null && (other.Latitude == Latitude && other.Longitude == Longitude);
		    //else
		}
	}
}
