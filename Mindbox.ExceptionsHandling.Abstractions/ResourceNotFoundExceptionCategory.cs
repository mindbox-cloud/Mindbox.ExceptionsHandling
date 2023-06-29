using System;
using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling;

public class ResourceNotFoundExceptionCategory : ExceptionCategory
{
	public ResourceNotFoundExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
	{
	}

	public override string Name => "ResourceNotFound";
	public override LogLevel LogLevel => LogLevel.Warning;
}