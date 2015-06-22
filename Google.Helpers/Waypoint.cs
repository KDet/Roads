using System;

namespace Google.Helpers
{
	[Obsolete("Functionality was absorbed by Location being polymorphic")]
	public class Waypoint
	{
		public LatitudeLongitude Position { get; set; }
		public string Address { get; set; }

		public Waypoint() { }

		public Waypoint(decimal lat, decimal lng)
		{
			Position = new LatitudeLongitude(lat, lng);
		}

		public override string ToString()
		{
			return Position != null ? Position.ToString() : Address;
		}
	}
}
