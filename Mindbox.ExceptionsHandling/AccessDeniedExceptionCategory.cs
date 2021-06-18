using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling
{
	public class AccessDeniedExceptionCategory : ExceptionCategory
	{
		public AccessDeniedExceptionCategory(
			Func<Exception, bool> exceptionFilter, LogLevel logLevel = LogLevel.Error) : base(exceptionFilter, logLevel)
		{
		}

		public override string Name => ExceptionCategoryNames.AccessDenied;
	}
}