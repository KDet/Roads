using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace LvivRoads.Core.Services.Internal
{
	public static class ConvertUtil
	{
        public static bool TryCast<TFrom, TTo>(IEnumerable<TFrom> collection, out IEnumerable<TTo> convertedCollection) where TTo : class
		{
		    var froms = collection as TFrom[] ?? collection.ToArray();
		    IEnumerator collectionEnumerator = froms.GetEnumerator();

			var target = new List<TTo>(froms.Count());

		    while (collectionEnumerator.MoveNext())
		        if (collectionEnumerator.Current == null)
		            target.Add(null);
		        else
		        {
		            var convert = collectionEnumerator.Current as TTo;
		            if (convert == null)
		            {
		                convertedCollection = null;
		                return false;
		            }
		            target.Add(convert);
		        }

		    convertedCollection = target;
			return true;
		}

        public static string HtmlToPlainText(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
	}
}
