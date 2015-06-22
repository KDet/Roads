using Newtonsoft.Json;

namespace Google.Helpers.Direction
{
	[JsonObject(MemberSerialization.OptIn)]
	public class Polyline
	{
		[JsonProperty("points")]
		public string Points { get; set; }

		[JsonProperty("levels")]
		public string Levels { get; set; }
	}
}
