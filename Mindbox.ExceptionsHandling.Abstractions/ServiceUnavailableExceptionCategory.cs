using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.Abstractions;

public class ServiceUnavailableExceptionCategory : ExceptionCategory
{
	public ServiceUnavailableExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
	{
	}

	public override string Name => ExceptionCategoryNames.ServiceUnavailable;
	public override LogLevel LogLevel => LogLevel.Warning;
}