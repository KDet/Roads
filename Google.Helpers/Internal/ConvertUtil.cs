using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Google.Helpers.Internal
{
	internal static class ConvertUtil
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

	}
}
