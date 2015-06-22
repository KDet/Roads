using System;
using LvivRoads.Core.Extensions;
using Newtonsoft.Json;

namespace LvivRoads.Core.Services
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Time
    {
        /// <summary>
        /// the time specified as Unix time, or seconds since midnight, January 1, 1970 UTC.
        /// </summary>
        [JsonProperty("value")]
        public double Value { get; set; }

        public DateTime UnixTimeValue
        {
            get { return Value.FromUnixTimestamp(); }
        }
        
        /// <summary>
        ///  the time specified as a string. The time is displayed in the time zone of the transit stop.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// contains the time zone of this station. The value is the name of the time zone as defined in the IANA Time Zone Database, e.g. "America/New_York".
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }
    }
}