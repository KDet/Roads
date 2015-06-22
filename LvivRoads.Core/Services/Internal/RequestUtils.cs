using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LvivRoads.Core.Services.Internal
{
	internal static class RequestUtils
	{
		public static string GetLatLngCollectionStr(IEnumerable<LatitudeLongitude> locationsCollection, int encodedPolylineThreshold = 3)
		{
			if (locationsCollection == null) return null;

		    var coordinates = locationsCollection as LatitudeLongitude[] ?? locationsCollection.ToArray();
		    int countOfItems = coordinates.Count();
			if (countOfItems >= encodedPolylineThreshold)
			{
				return Constants.PathEncodedPrefix + PolylineEncoder.EncodeCoordinates(coordinates);
			}
		    var sb = new StringBuilder(countOfItems * 22); // normally latlng's are -40.454545,-90.454545 so I picked a "larger than average" of 22 digits.
		    foreach (LatitudeLongitude position in coordinates)
		    {
		        if (sb.Length > 0) sb.Append("|");
		        sb.Append(position.GetAsUrlParameter());
		    }
		    return sb.ToString();
		}
	}
}
