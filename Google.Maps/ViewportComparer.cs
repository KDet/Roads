using System;
using System.Collections.Generic;
using Google.Maps.Geocoding;

namespace Google.Maps
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
		public Single Epsilon { get { return _epsilon; } }

		public bool Equals(Viewport x, Viewport y)
		{
			if (x == null || y == null) return false;

			if (x.Northeast == null || y.Northeast == null) return false;
			if (x.Southwest == null || y.Southwest == null) return false;

			LatLngComparer c = LatLngComparer.Within(Epsilon);
			if (c.Equals(x.Northeast, y.Northeast) == false)
				return false;

			if (c.Equals(x.Southwest, y.Southwest) == false)
				return false;

			return true;
		}
		public int GetHashCode(Viewport obj)
		{
			return obj.Southwest.GetHashCode() ^ obj.Northeast.GetHashCode();
		}
	}
}