using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling
{
	public class UnauthorizedAccessExceptionCategory : ExceptionCategory
	{
		public UnauthorizedAccessExceptionCategory(Func<Exception, bool> exceptionFilter) : base(exceptionFilter)
		{
		}

		public override string Name => ExceptionCategoryNames.UnauthorizedAccess;
		public override LogLevel LogLevel => LogLevel.Warning;
	}
}