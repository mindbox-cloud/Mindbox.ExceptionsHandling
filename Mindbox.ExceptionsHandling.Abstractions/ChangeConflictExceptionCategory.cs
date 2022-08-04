using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.Abstractions;

public class ChangeConflictExceptionCategory : ExceptionCategory
{
	public ChangeConflictExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
	{
	}

	public override string Name => ExceptionCategoryNames.ChangeConflict;
	public override LogLevel LogLevel => LogLevel.Warning;
}