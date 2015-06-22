using Newtonsoft.Json;

namespace Google.Helpers.DistanceMatrix
{
	/// <summary>
	/// 
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class DistanceMatrixResponse
	{
		[JsonProperty("status")]
		public ServiceResponseStatus Status { get; set; }

		[JsonProperty("rows")]
		public DistanceMatrixRows[] Rows { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonObject(MemberSerialization.OptIn)]
		public class DistanceMatrixRows
		{
			[JsonProperty("elements")]
			public DistanceMatrixElement[] Elements { get; set; }
		}//end class

		/// <summary>
		/// 
		/// </summary>
		[JsonObject(MemberSerialization.OptIn)]
		public class DistanceMatrixElement
		{

			[JsonProperty("status")]
			public ServiceResponseStatus Status { get; set; }

			[JsonProperty("distance")]
			public ValueText Distance { get; set; }

			[JsonProperty("duration")]
			public ValueText Duration { get; set; }
		}//end class
	}//end class
}//end namespace
