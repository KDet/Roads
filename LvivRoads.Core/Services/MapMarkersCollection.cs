using System.Collections.Generic;

namespace LvivRoads.Core.Services
{

	/// <summary>
	/// Contains a collection of <see cref="MapMarkers" />.
	/// </summary>
	public class MapMarkersCollection : List<MapMarkers>
	{
        public void Add(Position value)
		{
			var m = new MapMarkers();
			m.Locations.Add(value);
			Add(m);
		}

		//public static implicit operator MarkersCollection(LatLng value)
		//{
		//    MapMarkers m=new MapMarkers();
		//    m.Locations.Add(value);

		//    MarkersCollection c = new MarkersCollection();
		//    c.Add(m);

		//    return c;
		//}
		//public static implicit operator MarkersCollection(Location value)
		//{
		//    MapMarkers m = new MapMarkers();
		//    m.Locations.Add(value);

		//    MarkersCollection c = new MarkersCollection();
		//    c.Add(m);

		//    return c;
		//}


	}
}
