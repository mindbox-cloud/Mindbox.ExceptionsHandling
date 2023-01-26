using System;
using HotChocolate;
using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.GraphQL;

/// <summary>
/// Must be registered after other error filters in order for logging suppression to work.
/// </summary>
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
		LogError(categorizedError);
		return EnrichError(categorizedError);
	}

	private CategorizedError CategorizeError(IError error)
	{
		if (error.Exception is null or HotChocolate.Types.SerializationException)
		{
			return new CategorizedError(
				LogLevel.Error,
				ExceptionCategoryNames.InvalidRequest,
				error.Message,
				error);
		}
		else
		{
			var exceptionCategory = _exceptionCategoryMatcher.GetCategory(error.Exception);
			return new CategorizedError(
				exceptionCategory.LogLevel,
				exceptionCategory.Name,
				error.Exception.Message,
				error);
		}
	}

	private void LogError(CategorizedError categorizedError)
	{
		var shouldSuppressLogging =
			categorizedError.Error.Extensions?.TryGetValue("suppressLogging", out var suppressLogging) == true &&
			suppressLogging is true;

		if (!shouldSuppressLogging)
		{
			_logger.Log(categorizedError.LogLevel, categorizedError.Error.Exception, categorizedError.Message);
		}
	}

	private IError EnrichError(CategorizedError categorizedError)
	{
		return categorizedError.Error.Code != null
			? categorizedError.Error
			: ErrorBuilder.FromError(categorizedError.Error)
				.SetExceptionData(categorizedError.Error.Exception)
				.SetMessage(categorizedError.Message)
				.SetCode(categorizedError.CategoryName)
				.Build();
	}

	private record CategorizedError(LogLevel LogLevel, string CategoryName, string Message, IError Error);
}