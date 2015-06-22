using Newtonsoft.Json;

namespace Google.Helpers.Direction
{
	[JsonObject(MemberSerialization.OptIn)]
	public class DirectionLeg
	{
		[JsonProperty("steps")]
		public DirectionStep[] Steps { get; set; }

		[JsonProperty("duration")]
		public ValueText Duration { get; set; }

		[JsonProperty("distance")]
		public ValueText Distance { get; set; }

		[JsonProperty("start_location")]
		public LatitudeLongitude StartLocation { get; set; }

		[JsonProperty("end_location")]
		public LatitudeLongitude EndLocation { get; set; }

		[JsonProperty("start_address")]
		public string StartAddress { get; set; }

		[JsonProperty("end_address")]
		public string EndAddress { get; set; }
	}
}
