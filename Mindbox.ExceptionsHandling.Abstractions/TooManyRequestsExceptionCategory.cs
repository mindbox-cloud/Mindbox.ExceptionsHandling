using System;

using Microsoft.Extensions.Logging;

using Mindbox.ExceptionsHandling;

namespace Mindbox.ExceptionsHandling
{
	public class TooManyRequestsExceptionCategory : ExceptionCategory
	{
		public TooManyRequestsExceptionCategory(
			Func<Exception, bool> exceptionFilter, LogLevel logLevel = LogLevel.None) : base(exceptionFilter, logLevel)
		{
		}

		public override string Name => ExceptionCategoryNames.TooManyRequests;
	}
}