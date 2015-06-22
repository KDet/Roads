using Newtonsoft.Json;

namespace LvivRoads.Core.Services.Direction
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Vehicle
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("type")]
        public VehicleType VehicleType { get; set; }
    }
}