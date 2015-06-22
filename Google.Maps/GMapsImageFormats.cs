using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Maps
{
	/// <summary>
	/// Represents the different image formats available from the Google Maps API.
	/// </summary>
	public enum GMapsImageFormats
	{
		Unspecified = 0,
		/// <summary>
		/// (default) specifies the 8-bit PNG format
		/// </summary>
		Png = 1,
		/// <summary>
		/// specifies the 8-bit PNG format
		/// </summary>
		Png8 = 1,
		/// <summary>
		/// specifies the 32-bit PNG format
		/// </summary>
		Png32 = 2,
		/// <summary>
		/// specifies the GIF format
		/// </summary>
		Gif = 4,
		/// <summary>
		/// specifies the JPEG compression format
		/// </summary>
		Jpg = 5,
		/// <summary>
		/// specifies a non-progressive JPEG compression format
		/// </summary>
		JpGbaseline = 6
	}
}
