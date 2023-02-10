#nullable disable

using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling;

public class InfoExceptionCategory : ExceptionCategory
{
	public InfoExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
	{
	}

	public override string Name => ExceptionCategoryNames.Info;
	public override LogLevel LogLevel => LogLevel.Information;
}