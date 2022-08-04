using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.Abstractions;

public class InvalidRequestExceptionCategory : ExceptionCategory
{
	public InvalidRequestExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
	{
	}

	public override string Name => ExceptionCategoryNames.InvalidRequest;
	public override LogLevel LogLevel => LogLevel.Error;
}