using Newtonsoft.Json;

namespace LvivRoads.Core.Services
{
	[JsonObject(MemberSerialization.OptIn)]
	public class ValueText
	{
		[JsonProperty("value")]
        public double Value { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }
	}
}
