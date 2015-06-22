/* code reused from SoulSolutions */
/* retrieved from http://briancaos.wordpress.com/2009/10/16/google-maps-polyline-encoding-in-c/ */
/* implements the Polyline Encoding Algorithm as defined at 
 * http://code.google.com/apis/maps/documentation/utilities/polylinealgorithm.html 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace LvivRoads.Core.Services
{
	public class PolylineEncoder
	{
		/// <summary>
		/// Encodes the list of coordinates to a Google Maps encoded coordinate string.
		/// </summary>
		/// <param name="coordinates">The coordinates.</param>
		/// <returns>Encoded coordinate string</returns>
		public static string EncodeCoordinates(IEnumerable<LatitudeLongitude> coordinates)
		{
			double oneEFive = Convert.ToDouble(1e5);

			int lat = 0;
			int lng = 0;
			var encodedCoordinates = new StringBuilder();

			foreach (LatitudeLongitude coordinate in coordinates)
			{
				// Round to 5 decimal places and drop the decimal
				var late5 = (int)(coordinate.Latitude * oneEFive);
				var lng5 = (int)(coordinate.Longitude * oneEFive);

				// Encode the differences between the coordinates
				encodedCoordinates.Append(EncodeSignedNumber(late5 - lat));
				encodedCoordinates.Append(EncodeSignedNumber(lng5 - lng));

				// Store the current coordinates
				lat = late5;
				lng = lng5;
			}
            return encodedCoordinates.ToString();
		}

		/// <summary>
		/// Decode encoded polyline information to a collection of <see cref="LatitudeLongitude"/> instances.
		/// </summary>
		/// <param name="value">ASCII string</param>
		/// <returns></returns>
		public static IEnumerable<LatitudeLongitude> Decode(string value)
		{
			//decode algorithm adapted from saboor awan via codeproject:
			//http://www.codeproject.com/Tips/312248/Google-Maps-Direction-API-V3-Polyline-Decoder
			//note the Code Project Open License at http://www.codeproject.com/info/cpol10.aspx

			if (string.IsNullOrEmpty(value)) 
                yield break;
			
			char[] polylineChars = value.ToCharArray();
			int index = 0;

			int currentLat = 0;
			int currentLng = 0;

		    //var poly = new List<LatitudeLongitude>();
            while (index < polylineChars.Length)
			{
				// calculate next latitude
				int sum = 0;
				int shifter = 0;
			    int next5Bits;
			    do
				{
					next5Bits = polylineChars[index++] - 63;
					sum |= (next5Bits & 31) << shifter;
					shifter += 5;
				} while (next5Bits >= 32 && index < polylineChars.Length);

				if (index >= polylineChars.Length)
					break;

				currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

				//calculate next longitude
				sum = 0;
				shifter = 0;
				do
				{
					next5Bits = polylineChars[index++] - 63;
					sum |= (next5Bits & 31) << shifter;
					shifter += 5;
				} while (next5Bits >= 32 && index < polylineChars.Length);

				if (index >= polylineChars.Length && next5Bits >= 32)
					break;

				currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
				var point = new LatitudeLongitude(
					latitude: Convert.ToDouble(currentLat) / 100000.0,
					longitude: Convert.ToDouble(currentLng) / 100000.0
				);
				yield return point;
			}
          //  return poly;
		}

		/// <summary>
		/// Encode a signed number in the encode format.
		/// </summary>
		/// <param name="num">The signed number</param>
		/// <returns>The encoded string</returns>
		private static string EncodeSignedNumber(int num)
		{
			int sgnNum = num << 1; //shift the binary value
			if (num < 0) //if negative invert
			{
				sgnNum = ~(sgnNum);
			}
			return (EncodeNumber(sgnNum));
		}

		/// <summary>
		/// Encode an unsigned number in the encode format.
		/// </summary>
		/// <param name="num">The unsigned number</param>
		/// <returns>The encoded string</returns>
		private static string EncodeNumber(int num)
		{
			var encodeString = new StringBuilder();
			while (num >= 0x20)
			{
				encodeString.Append((char)((0x20 | (num & 0x1f)) + 63));
				num >>= 5;
			}
			encodeString.Append((char)(num + 63));
			// All backslashes needs to be replaced with double backslashes
			// before being used in a Javascript string.
			return encodeString.ToString();
		}
	}
}
