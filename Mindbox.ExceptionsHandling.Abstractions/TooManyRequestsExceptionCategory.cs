using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling;

public class TooManyRequestsExceptionCategory : ExceptionCategory
{
	public TooManyRequestsExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
	{
	}

	public override string Name => ExceptionCategoryNames.TooManyRequests;
	public override LogLevel LogLevel => LogLevel.None;
}