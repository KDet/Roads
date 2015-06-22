using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LvivRoads.Core.Services
{
	internal static class Constants
	{
		public const int SizeWidthMin = 1;
		public const int SizeHeightMin = 1;
		public const int SizeWidthMax = 4096;
		public const int SizeHeightMax = 4096;

		public const int ZoomLevelMin = 0;

		public const string PathEncodedPrefix = "enc:";
		public const string PipeUrlEncoded = "%7C";

		public const string ExpectedColors = "black, brown, green, purple, yellow, blue, gray, orange, red, white"; //pasted straight from the website.

		private static readonly string[] ExpectedNamedColors;
		public static bool IsExpectedNamedColor(string value)
		{
			if (value == null) return false;
			return (Contains(ExpectedNamedColors, value, StringComparison.OrdinalIgnoreCase));
		}

		private static readonly int[] ExpectedScaleValues;
		public static bool IsExpectedScaleValue(int value, bool throwIfOutOfRange)
		{
			if (Contains(ExpectedScaleValues, value)) 
                return true;
            if (throwIfOutOfRange)
				throw new ArgumentOutOfRangeException("Scale value can only be " + ListValues(ExpectedScaleValues));
		    return false;
		}

		static Constants()
		{
			ExpectedNamedColors = ExpectedColors.Replace(", ", ",").Split(',');  //since we paste straight from the website, we remove spaces, and convert to an array.
			ExpectedScaleValues = new[] { 1, 2, 4 };
		}

		#region Pre-Framework v3.0 support
		private static bool Contains(IEnumerable<string> array, string value, StringComparison ignoreCase)
		{
		    return array.Any(t => string.Compare(t, value, ignoreCase) == 0);
		}
        
	    private static bool Contains(IEnumerable<int> array, int value)
	    {
	       return array.Any(t => t == value);
	    }

	    private static string ListValues(IEnumerable<int> array)
		{
			var sb = new StringBuilder();
			foreach (int element in array)
			{
			    if (sb.Length > 0) 
                    sb.Append(",");
			    sb.Append(element);
			}
			return sb.ToString();
		}
		#endregion
	}
}
