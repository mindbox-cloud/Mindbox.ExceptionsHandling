using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling;

public class ServiceUnavailableExceptionCategory : ExceptionCategory
{
	public ServiceUnavailableExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
	{
	}

	public override string Name => ExceptionCategoryNames.ServiceUnavailable;
	public override LogLevel LogLevel => LogLevel.Warning;
}