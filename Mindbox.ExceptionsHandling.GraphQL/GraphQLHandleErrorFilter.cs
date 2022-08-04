using System;
using HotChocolate;
using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.GraphQL;

public class GraphQLHandleErrorFilter : IErrorFilter
{
	private readonly IExceptionCategoryMatcher _exceptionCategoryMatcher;
	private readonly ILogger _logger;

	public GraphQLHandleErrorFilter(IExceptionCategoryMatcher exceptionCategoryMatcher, ILoggerFactory loggerFactory)
	{
		if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));

		_exceptionCategoryMatcher = exceptionCategoryMatcher
			?? throw new ArgumentNullException(nameof(exceptionCategoryMatcher));
		_logger = loggerFactory.CreateLogger<GraphQLHandleErrorFilter>();
	}

	public IError OnError(IError error)
	{
		if (error == null)
			throw new ArgumentNullException(nameof(error));

		var categorizedError = CategorizeError(error);
		LogError(categorizeError);
		return EnrichError(error, categorizeError);
	}

	private CategorizedError CategorizeError(IError error)
	{
		if (error.Exception is null or HotChocolate.Types.SerializationException)
		{
			return new CategorizedError(
				LogLevel.Error,
				ExceptionCategoryNames.InvalidRequest,
				error.Message,
				Exception: null);
		}
		else
		{
			var exceptionCategory = _exceptionCategoryMatcher.GetCategory(error.Exception);
			return new CategorizedError(
				exceptionCategory.LogLevel,
				exceptionCategory.Name,
				error.Exception.Message,
				error.Exception);
		}
	}

	private void LogError(CategorizedError categorizedError)
	{
		_logger.Log(categorizedError.LogLevel, categorizedError.Exception, categorizedError.Message);
	}

	private IError EnrichError(IError error, CategorizedError categorizedError)
	{
		return error.Code != null
			? error
			: ErrorBuilder.FromError(error)
				.SetMessage(categorizedError.Message)
				.SetCode(categorizedError.CategoryName)
				.Build();
	}

	private record CategorizedError(LogLevel LogLevel, string CategoryName, string Message, Exception? Exception);
}