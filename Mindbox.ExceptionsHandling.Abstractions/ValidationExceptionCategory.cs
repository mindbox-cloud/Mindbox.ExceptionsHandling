using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling;

public class ValidationExceptionCategory : ExceptionCategory
{
	public ValidationExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
	{
	}

	public override string Name => ExceptionCategoryNames.ValidationException;
	public override LogLevel LogLevel => LogLevel.None;
}