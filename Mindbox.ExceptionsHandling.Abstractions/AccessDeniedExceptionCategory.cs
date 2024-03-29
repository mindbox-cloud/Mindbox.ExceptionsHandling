using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling;

public class AccessDeniedExceptionCategory : ExceptionCategory
{
	public AccessDeniedExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
	{
	}

	public override string Name => ExceptionCategoryNames.AccessDenied;
	public override LogLevel LogLevel => LogLevel.Warning;
}