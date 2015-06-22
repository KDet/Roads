using Newtonsoft.Json;

namespace LvivRoads.Core.Services.Direction
{
    public enum VehicleType
    {
        /// <summary>
        /// Rail.
        /// </summary>
        [JsonProperty("RAIL")]
        Rail,
        
        /// <summary>
        /// Light rail transit.
        /// </summary>
        [JsonProperty("METRO_RAIL")]
        MetroRail,
        
        /// <summary>
        /// Underground light rail.
        /// </summary>
        [JsonProperty("SUBWAY")]
        Subway,
        
        /// <summary>
        /// Above ground light rail.
        /// </summary>
        [JsonProperty("TRAM")]
        Tram,

        /// <summary>
        /// Monorail.
        /// </summary>
        [JsonProperty("MONORAIL")]
        Monorail,

        /// <summary>
        /// Heavy rail.
        /// </summary>
        [JsonProperty("HEAVY_RAIL")]
        HeavyRail,

        /// <summary>
        /// Commuter rail.
        /// </summary>
        [JsonProperty("COMMUTER_TRAIN")]
        CommuterTrain,

        /// <summary>
        /// High speed train.
        /// </summary>
        [JsonProperty("HIGH_SPEED_TRAIN")]
        HighSpeedTrain,

        /// <summary>
        /// Bus.
        /// </summary>
        [JsonProperty("BUS")]
        Bus, //	Bus.

        /// <summary>
        /// Intercity bus.
        /// </summary>
        [JsonProperty("INTERCITY_BUS")]
        IntercityBus,

        /// <summary>
        /// Trolleybus.
        /// </summary>
        [JsonProperty("TROLLEYBUS")]
        Trolleybus,

        /// <summary>
        /// Share taxi is a kind of bus with the ability to drop off and pick up passengers anywhere on its route.
        /// </summary>
        [JsonProperty("SHARE_TAXI")]
        ShareTaxi,

        /// <summary>
        /// Ferry.
        /// </summary>
        [JsonProperty("FERRY")]
        Ferry,
        
        /// <summary>
        /// A vehicle that operates on a cable, usually on the ground. Aerial cable cars may be of the type GONDOLA_LIFT.
        /// </summary>
        [JsonProperty("CABLE_CAR")]
        CableCar,
        
        /// <summary>
        /// An aerial cable car.
        /// </summary>
        [JsonProperty("GONDOLA_LIFT")]
        GondolaLift,

        /// <summary>
        /// A vehicle that is pulled up a steep incline by a cable. A Funicular typically consists of two cars, with each car acting as a counterweight for the other.
        /// </summary>
        [JsonProperty("FUNICULAR")]
        Funicular,

        /// <summary>
        /// All other vehicles will return this type.
        /// </summary>
        [JsonProperty("OTHER")]
        Other,
    }
}