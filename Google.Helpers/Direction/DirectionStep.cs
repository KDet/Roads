using Newtonsoft.Json;

namespace Google.Helpers.Direction
{
	[JsonObject(MemberSerialization.OptIn)]
	public class DirectionStep
	{
		[JsonProperty("travel_mode")]
		public TravelMode TravelMode { get; set; }

		[JsonProperty("start_location")]
		public LatitudeLongitude StartLocation { get; set; }

		[JsonProperty("end_location")]
		public LatitudeLongitude EndLocation { get; set; }

		[JsonProperty("polyline")]
		public Polyline Polyline { get; set; }

		[JsonProperty("duration")]
		public ValueText Duration { get; set; }

		[JsonProperty("html_instructions")]
		public string HtmlInstructions { get; set; }

		[JsonProperty("distance")]
		public ValueText Distance { get; set; }

		public DirectionStep() { }

		public DirectionStep(LatitudeLongitude start, LatitudeLongitude end)
		{
			StartLocation = start;
			EndLocation = end;
		}

		public DirectionStep(decimal startLat, decimal startLng, decimal endLat, decimal endLng)
		{
			StartLocation = new LatitudeLongitude(startLat, startLng);
			EndLocation = new LatitudeLongitude(endLat, endLng);
		}
	}
}
