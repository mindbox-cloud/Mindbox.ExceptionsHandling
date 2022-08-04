using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.Abstractions;

public abstract class ExceptionCategory : IExceptionCategory
{
	private readonly Func<Exception, bool> _exceptionFilter;

	protected ExceptionCategory(Func<Exception, bool> exceptionFilter)
	{
		_exceptionFilter = exceptionFilter;
	}

	public abstract string Name { get; }
	public abstract LogLevel LogLevel { get; }

	public bool DoesMatchTopException(Exception exception) => _exceptionFilter(exception);
}