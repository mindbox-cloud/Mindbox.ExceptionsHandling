using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling
{
	public class InvalidRequestExceptionCategory : ExceptionCategory
	{
		public InvalidRequestExceptionCategory(
			Func<Exception, bool> exceptionFilter, LogLevel logLevel = LogLevel.Error) : base(exceptionFilter, logLevel)
		{
		}

		public override string Name => ExceptionCategoryNames.InvalidRequest;
	}
}