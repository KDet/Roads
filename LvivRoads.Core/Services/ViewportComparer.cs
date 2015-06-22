using System.Collections.Generic;
using LvivRoads.Core.Services.Geocoding;

namespace LvivRoads.Core.Services
{
	public class ViewportComparer : IEqualityComparer<Viewport>
	{

		public static ViewportComparer Within(float epsilon)
		{
			return new ViewportComparer(epsilon);
		}

		private ViewportComparer(float epsilon)
		{
			_epsilon = epsilon;
		}

	    readonly float _epsilon;
		public float Epsilon { get { return _epsilon; } }


		public bool Equals(Viewport x, Viewport y)
		{
			if (x == null || y == null) return false;

			if (x.Northeast == null || y.Northeast == null) return false;
			if (x.Southwest == null || y.Southwest == null) return false;

			LatitudeLongitudeComparer c = LatitudeLongitudeComparer.Within(Epsilon);
		    return c.Equals(x.Northeast, y.Northeast) &&
		           c.Equals(x.Southwest, y.Southwest);
		}

		public int GetHashCode(Viewport obj)
		{
			return obj.Southwest.GetHashCode() ^ obj.Northeast.GetHashCode();
		}
	}
}
