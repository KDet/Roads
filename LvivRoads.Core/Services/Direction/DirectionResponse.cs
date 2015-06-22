using Newtonsoft.Json;

namespace LvivRoads.Core.Services.Direction
{
	[JsonObject(MemberSerialization.OptIn)]
	public class DirectionResponse
	{
		[JsonProperty("status")]
		public ServiceResponseStatus Status { get; set; }

		[JsonProperty("routes")]
		public DirectionRoute[] Routes { get; set; }
	}
}
