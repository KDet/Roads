using Newtonsoft.Json;

namespace LvivRoads.Core.Services.Direction
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StopStation
    {
        /// <summary>
        /// name the name of the transit station/stop. eg. "Union Square".
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// location the location of the transit station/stop, represented as a lat and lng field.
        /// </summary>
        [JsonProperty("location")]
        public LatitudeLongitude Location { get; set; }
    }
}