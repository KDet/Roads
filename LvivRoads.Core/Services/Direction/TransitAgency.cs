using System;
using Newtonsoft.Json;

namespace LvivRoads.Core.Services.Direction
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TransitAgency
    {
        /// <summary>
        /// name contains the name of the transit agency.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// url contains the URL for the transit agency.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// phone contains the phone number of the transit agency.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}