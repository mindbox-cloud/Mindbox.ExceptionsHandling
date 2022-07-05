using System;
using System.Collections.Generic;
using System.Linq;

namespace Mindbox.ExceptionsHandling;

public static class ExceptionExtensions
{
	public static IEnumerable<Exception> GetAllExceptions(this Exception exception)
	{
		yield return exception;

		if (exception is AggregateException aggregateException)
		{
			foreach (var innerException in aggregateException.InnerExceptions.SelectMany(GetAllExceptions))
			{
				yield return innerException;
			}
		}
		else if (exception.InnerException != null)
		{
			foreach (var innerException in GetAllExceptions(exception.InnerException))
			{
				yield return innerException;
			}
		}
	}
}