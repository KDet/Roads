﻿using Newtonsoft.Json;

namespace Google.Helpers
{
	[JsonObject(MemberSerialization.OptIn)]
	public class ValueText
	{
		[JsonProperty("value")]
		public string Value { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }
	}
}
