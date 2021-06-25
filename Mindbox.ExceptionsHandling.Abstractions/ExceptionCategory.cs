using System;

using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling
{
	public abstract class ExceptionCategory : IExceptionCategory
	{
		private readonly Func<Exception, bool> exceptionFilter;

		protected ExceptionCategory(Func<Exception, bool> exceptionFilter, LogLevel logLevel)
		{
			this.exceptionFilter = exceptionFilter;
			LogLevel = logLevel;
		}
		
		public abstract string Name { get; }
		public LogLevel LogLevel { get; }

		public bool DoesMatchTopException(Exception exception) => exceptionFilter(exception);
	}
}