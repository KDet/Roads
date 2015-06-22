using System;
using System.Collections.Generic;

namespace LvivRoads.Core.Services
{
	public class LatitudeLongitudeComparer : IEqualityComparer<LatitudeLongitude>
	{
        public static LatitudeLongitudeComparer Within(float epsilon)
		{
			return new LatitudeLongitudeComparer(epsilon);
		}

		private LatitudeLongitudeComparer(float epsilon)
		{
			_epsilon = epsilon;
		}

	    readonly Single _epsilon;
		public Single Epsilon { get { return _epsilon; } }

		public bool Equals(LatitudeLongitude x, LatitudeLongitude y)
		{
			if (x == null || y == null) return false;

			if(Equals(x.Latitude,y.Latitude, _epsilon)==false)
				return false;

			if (Equals(x.Longitude, y.Longitude, _epsilon) == false)
				return false;

			return true;
		}

		public int GetHashCode(LatitudeLongitude value)
		{
			return value.Latitude.GetHashCode() ^ value.Longitude.GetHashCode();
		}

		private static bool Equals(double a, double b, float epsilonParam)
		{
			double epsilon = Convert.ToDouble(epsilonParam);
			double absA = Math.Abs(a);
			double absB = Math.Abs(b);
			double diff = Math.Abs(a - b);

			if (Math.Abs(a * b) < epsilonParam)
			{ // a or b or both are zero
				// relative error is not meaningful here
				return diff < (epsilon * epsilon);
			}
		    // use relative error
		    return diff / (absA + absB) < epsilon;
		}
	}
}
