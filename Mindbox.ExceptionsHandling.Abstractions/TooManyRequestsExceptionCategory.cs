using System;

using Microsoft.Extensions.Logging;

using Mindbox.ExceptionsHandling;

namespace Mindbox.ExceptionsHandling
{
	public class TooManyRequestsExceptionCategory : ExceptionCategory
	{
		public TooManyRequestsExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
		{
		}

		public override string Name => ExceptionCategoryNames.TooManyRequests;
		public override LogLevel LogLevel => LogLevel.None;
	}
}