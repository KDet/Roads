using Newtonsoft.Json;

namespace LvivRoads.Core.Services.Direction
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TransitLine
    {
        /// <summary>
        /// name contains the full name of this transit line. eg. "7 Avenue Express".
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// short_name contains the short name of this transit line. This will normally be a line number, such as "M7" or "355".
        /// </summary>
        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        /// <summary>
        /// color contains the color commonly used in signage for this transit line. The color will be specified as a hex string such as: #FF0033.
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// agencies contains an array of TransitAgency objects that each provide information about the operator of the line
        /// </summary>
        [JsonProperty("agencies")]
        public TransitAgency[] Agencies { get; set; }

        /// <summary>
        /// url contains the URL for this transit line as provided by the transit agency.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// url contains the URL for this transit line as provided by the transit agency.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// text_color contains the color of text commonly used for signage of this line. The color will be specified as a hex string.
        /// </summary>
        [JsonProperty("text_color")]
        public string TextColor { get; set; }

        [JsonProperty("vehicle")]
        public Vehicle Vehicle { get; set; }
    }
}