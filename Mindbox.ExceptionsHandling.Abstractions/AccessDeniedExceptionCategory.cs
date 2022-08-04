using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.Abstractions;

public class AccessDeniedExceptionCategory : ExceptionCategory
{
	public AccessDeniedExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
	{
	}

	public override string Name => ExceptionCategoryNames.AccessDenied;
	public override LogLevel LogLevel => LogLevel.Error;
}