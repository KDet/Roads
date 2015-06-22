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
using System.Net;

namespace LvivRoads.Core.Services.Geocoding
{
	/// <summary>
	/// Provides a request for the Google Maps Geocoding web service.
	/// </summary>
	public class GeocodingRequest
	{
		/// <summary>
		/// The address that you want to geocode.  Use LatLng to perform a reverse geocoding request.
		/// </summary>
		/// <see cref="http://code.google.com/apis/maps/documentation/geocoding/#ReverseGeocoding"/>
		/// <remarks>Required if latlng not present.</remarks>
        public Position Address { get; set; }

		/// <summary>
		/// Undocumented address component filters.
		/// Only geocoding results matching the component filters will be returned.
		/// </summary>
		/// <remarks>IE: country:uk|locality:stathern</remarks>
		public string Components { get; set; }

		/// <summary>
		/// The bounding box of the viewport within which to bias geocode
		/// results more prominently.
		/// </summary>
		/// <remarks>
		/// Optional. This parameter will only influence, not fully restrict, results
		/// from the geocoder.
		/// </remarks>
		/// <see cref="http://code.google.com/apis/maps/documentation/geocoding/#Viewports"/>
		public Viewport Bounds { get; set; }

		/// <summary>
		/// The region code, specified as a ccTLD ("top-level domain")
		/// two-character value.
		/// </summary>
		/// <remarks>
		/// Optional. This parameter will only influence, not fully restrict, results
		/// from the geocoder.
		/// </remarks>
		/// <see cref="http://code.google.com/apis/maps/documentation/geocoding/#RegionCodes"/>
		public string Region { get; set; }

		/// <summary>
		/// The language in which to return results. If language is not
		/// supplied, the geocoder will attempt to use the native language of
		/// the domain from which the request is sent wherever possible.
		/// </summary>
		/// <remarks>Optional.</remarks>
		/// <see cref="http://code.google.com/apis/maps/faq.html#languagesupport"/>
		public string Language { get; set; }

		/// <summary>
		/// Indicates whether or not the geocoding request comes from a device
		/// with a location sensor. This value must be either true or false.
		/// </summary>
		/// <remarks>Required.</remarks>
		public bool? Sensor { get; set; }

		internal Uri ToUri()
		{
			EnsureSensor();
			if (Address == null) throw new InvalidOperationException("Address property is not set.");
            var qsb = new Internal.QueryStringBuilder();
            qsb.Append(Address.GetType() == typeof (LatitudeLongitude) ? "latlng" : "address",
		        Address.GetAsUrlParameter());
            qsb.Append("bounds", GetBoundsStr())
                .Append("components", WebUtility.UrlEncode(Components))
				.Append("region", Region)
				.Append("language", Language)
				.Append("sensor", ((Sensor != null && Sensor.Value).ToString().ToLowerInvariant()));
            var url = string.Format("json?{0}", qsb);
            return new Uri(url, UriKind.Relative);
		}

		private string GetBoundsStr()
		{
			if (Bounds == null) return null;
            string swStr = Bounds.Southwest.GetAsUrlParameter();
			string neStr = Bounds.Northeast.GetAsUrlParameter();
            return string.Format("{0}{1}{2}", swStr, Constants.PipeUrlEncoded, neStr);
		}

		private void EnsureSensor()
		{
			if (Sensor == null) 
                throw new InvalidOperationException("Sensor property hasn't been set.");
		}
	}
}
