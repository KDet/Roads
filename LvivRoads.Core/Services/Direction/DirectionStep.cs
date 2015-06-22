using System;
using Newtonsoft.Json;

namespace LvivRoads.Core.Services.Direction
{
	[JsonObject(MemberSerialization.OptIn)]
	public class DirectionStep
	{
		[JsonProperty("travel_mode")]
		public TravelMode TravelMode { get; set; }

        /// <summary>
        /// start_location contains the location of the starting point of this step, as a single set of lat and lng fields.
        /// </summary>
		[JsonProperty("start_location")]
		public LatitudeLongitude StartLocation { get; set; }

        /// <summary>
        /// end_location contains the location of the last point of this step, as a single set of lat and lng fields.
        /// </summary>
		[JsonProperty("end_location")]
		public LatitudeLongitude EndLocation { get; set; }

		[JsonProperty("polyline")]
		public Polyline Polyline { get; set; }

        /// <summary>
        /// duration contains the typical time required to perform the step, until the next step
        /// </summary>
		[JsonProperty("duration")]
		public ValueText Duration { get; set; }

        /// <summary>
        /// html_instructions contains formatted instructions for this step, presented as an HTML text string.
        /// </summary>
		[JsonProperty("html_instructions")]
		public string HtmlInstructions { get; set; }

        /// <summary>
        /// distance contains the distance covered by this step until the next step
        /// </summary>
		[JsonProperty("distance")]
		public ValueText Distance { get; set; }

        /// <summary>
        /// sub_steps contains detailed directions for walking or driving steps in transit directions. Substeps are only available when travel_mode is set to "transit". The sub_steps array is of the same type as steps.
        /// </summary>
        [JsonProperty("sub_steps")]
        public DirectionStep[] SubSteps { get; set; }

        /// <summary>
        /// transit_details contains transit specific information. This field is only returned with travel_mode is set to "transit". See Transit Details below.
        /// </summary>
        [JsonProperty("transit_details")]
        public TransitDetails TransitDetails { get; set; }

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
