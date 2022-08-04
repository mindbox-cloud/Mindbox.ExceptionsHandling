using System;
using System.Collections.Generic;
using System.Linq;
using Mindbox.ExceptionsHandling.Abstractions;

namespace Mindbox.ExceptionsHandling;

public class ExceptionCategoryMatcher : IExceptionCategoryMatcher
{
	public ExceptionCategoryMatcher(IEnumerable<IExceptionCategory> categories)
	{
		if (categories == null)
			throw new ArgumentNullException(nameof(categories));
		if (!categories.Any())
			throw new ArgumentException("At least one category is required", nameof(categories));

		Categories = categories.ToArray();
	}

	protected IReadOnlyCollection<IExceptionCategory> Categories { get; }

	public IExceptionCategory GetCategory(Exception exception)
	{
		if (exception == null)
			throw new ArgumentNullException(nameof(exception));

		return exception
			.GetAllExceptions()
			.Select(TryGetTopCategory)
			.FirstOrDefault(category => category != null) ?? DefaultExceptionCategory.Instance;
	}

	public virtual IExceptionCategory? TryGetTopCategory(Exception exception)
	{
		if (exception == null)
			throw new ArgumentNullException(nameof(exception));

		return Categories.FirstOrDefault(category => category.DoesMatchTopException(exception));
	}

	public bool HasCategory<TExceptionCategory>(Exception exception) where TExceptionCategory : IExceptionCategory
	{
		if (exception == null)
			throw new ArgumentNullException(nameof(exception));

		return GetCategory(exception) is TExceptionCategory;
	}
}