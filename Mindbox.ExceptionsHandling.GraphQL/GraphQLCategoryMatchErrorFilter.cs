using System;
using HotChocolate;

namespace Mindbox.ExceptionsHandling.GraphQL;

public class GraphQLCategoryMatchErrorFilter : IErrorFilter
{
	private readonly IExceptionCategoryMatcher _exceptionCategoryMatcher;

	public GraphQLCategoryMatchErrorFilter(IExceptionCategoryMatcher exceptionCategoryMatcher)
	{
		_exceptionCategoryMatcher = exceptionCategoryMatcher
			?? throw new ArgumentNullException(nameof(exceptionCategoryMatcher));
	}

	public IError OnError(IError error)
	{
		if (error == null)
			throw new ArgumentNullException(nameof(error));
		return error.Code != null
			? error
			: ProcessError(error);
	}

	private IError ProcessError(IError error)
	{
		if (error.Exception is null)
		{
			return ErrorBuilder.FromError(error)
				.SetMessage(error.Message)
				.SetCode(ExceptionCategoryNames.InvalidRequest)
				.Build();
		}
		else
		{
			var errorCategory = _exceptionCategoryMatcher.GetCategory(error.Exception);
			var errorCode = errorCategory.Name;

			return ErrorBuilder.FromError(error)
				.SetMessage(error.Exception.Message)
				.SetCode(errorCode)
				.Build();
		}
	}
}