using Microsoft.Extensions.Options;
using Sentry;
using Sentry.Extensibility;

namespace Mindbox.ExceptionsHandling.Template;

public class SentryExceptionsCategoryProcessor
	: ISentryEventProcessor
{
	private readonly IExceptionCategoryMatcher _exceptionCategoryMatcher;
	private readonly IOptions<SentryOptions> _sentryOptions;

	public SentryExceptionsCategoryProcessor(
		IExceptionCategoryMatcher exceptionCategoryMatcher,
		IOptions<SentryOptions> sentryOptions)
	{
		_exceptionCategoryMatcher = exceptionCategoryMatcher;
		_sentryOptions = sentryOptions;
	}

	public SentryEvent? Process(SentryEvent @event)
	{
		if (@event.Exception != null)
		{
			var category = _exceptionCategoryMatcher.TryGetTopCategory(@event.Exception);
			if (category != null && category.LogLevel < _sentryOptions.Value.MinimumEventLevel) return null;
		}

		return @event;
	}
}