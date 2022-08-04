using System;
using Microsoft.Extensions.Logging;
using Mindbox.ExceptionsHandling.Abstractions;

namespace Mindbox.ExceptionsHandling;

public class ExceptionLogger : IExceptionLogger
{
	private readonly IExceptionCategoryMatcher _exceptionCategoryMatcher;

	private readonly ILogger _logger;

	public ExceptionLogger(ILogger logger, IExceptionCategoryMatcher exceptionCategoryMatcher)
	{
		_logger = logger;
		_exceptionCategoryMatcher = exceptionCategoryMatcher;
	}

	public void Log(Exception exception)
	{
		var exceptionCategory = _exceptionCategoryMatcher.GetCategory(exception);
		_logger.Log(exceptionCategory.LogLevel, exception, exception.Message);
	}
}