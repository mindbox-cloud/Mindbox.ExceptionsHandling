using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling
{
	public class ChangeConflictExceptionCategory : ExceptionCategory
	{
		public ChangeConflictExceptionCategory(
			Func<Exception, bool> exceptionFilter, LogLevel logLevel = LogLevel.Warning) : base(exceptionFilter, logLevel)
		{
		}

		public override string Name => ExceptionCategoryNames.ChangeConflict;
	}
}