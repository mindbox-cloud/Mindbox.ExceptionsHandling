using System;
using System.Collections;

namespace Mindbox.ExceptionsHandling
{
	public interface IExceptionCategoryMatcher
	{
		/// <summary>
		/// Tries to get the matched category. Looks at inner exceptions too. Returns the default category if none match. 
		/// </summary>
		/// <param name="exception">Error for which need to define the category</param>
		/// <returns>Returns matched category or default category if none match</returns>
		IExceptionCategory GetCategory(Exception exception);

		/// <summary>
		/// Tries to get the matched category. Does not look at inner exceptions.
		/// </summary>
		/// <param name="exception">Error for which need to define the category</param>
		/// <returns>Returns matched category or null if none match</returns>
		IExceptionCategory? TryGetTopCategory(Exception exception);

		bool HasCategory<TExceptionCategory>(Exception exception) where TExceptionCategory : IExceptionCategory;
	}
}
