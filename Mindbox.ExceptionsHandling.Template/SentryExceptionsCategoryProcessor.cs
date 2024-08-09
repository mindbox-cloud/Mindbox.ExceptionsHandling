using Microsoft.Extensions.Options;
using Sentry;
using Sentry.Extensibility;

namespace Mindbox.ExceptionsHandling.Template;

public class SentryExceptionsCategoryProcessor(
	IExceptionCategoryMatcher exceptionCategoryMatcher,
	IOptions<SentryOptions> sentryOptions)
	: ISentryEventProcessor
{
	public SentryEvent? Process(SentryEvent @event)
	{
		if (@event.Exception != null)
		{
			var category = exceptionCategoryMatcher.TryGetTopCategory(@event.Exception);
			if (category != null && category.LogLevel < sentryOptions.Value.MinimumEventLevel) return null;
		}

		return @event;
	}
}