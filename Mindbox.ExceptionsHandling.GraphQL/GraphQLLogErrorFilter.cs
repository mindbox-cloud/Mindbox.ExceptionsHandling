using System;
using HotChocolate;
using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.GraphQL;

public class GraphQLLogErrorFilter : IErrorFilter
{
	private readonly IExceptionCategoryMatcher _exceptionCategoryMatcher;
	private readonly ILogger _logger;

	public GraphQLLogErrorFilter(IExceptionCategoryMatcher exceptionCategoryMatcher, ILoggerFactory loggerFactory)
	{
		if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));

		_exceptionCategoryMatcher = exceptionCategoryMatcher
		                            ?? throw new ArgumentNullException(nameof(exceptionCategoryMatcher));

		_logger = loggerFactory.CreateLogger<GraphQLLogErrorFilter>();
	}

	public IError OnError(IError error)
	{
		if (error == null)
			throw new ArgumentNullException(nameof(error));
		return ProcessError(error);
	}

	private IError ProcessError(IError error)
	{
		if (error.Exception is null or HotChocolate.Types.SerializationException)
		{
			_logger.Log(LogLevel.Error, error.Message);
		}
		else
		{
			var exceptionCategory = _exceptionCategoryMatcher.GetCategory(error.Exception);
			_logger.Log(exceptionCategory.LogLevel, error.Exception, error.Exception.Message);
		}

		return error;
	}
}