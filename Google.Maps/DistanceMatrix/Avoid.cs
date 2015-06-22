using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Services;

namespace Google.Maps.DistanceMatrix
{
	/// <summary>
	/// Directions may be calculated that adhere to certain restrictions. 
	/// Restrictions are indicated by use of the avoid parameter, and an 
	/// argument to that parameter indicating the restriction to avoid.
	/// </summary>
	public enum Avoid
	{
		/// <summary>
		/// 
		/// </summary>
		Highways,

		/// <summary>
		/// 
		/// </summary>
		Tolls
	}
}
