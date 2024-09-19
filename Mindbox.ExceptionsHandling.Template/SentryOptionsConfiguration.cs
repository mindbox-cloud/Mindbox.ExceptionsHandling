using Microsoft.Extensions.Options;
using Sentry.AspNetCore;
using Sentry.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.Template;

internal class SentryOptionsConfiguration : IConfigureOptions<SentryAspNetCoreOptions>, IConfigureOptions<SentryLoggingOptions>
{
	private readonly SentryExceptionsCategoryProcessor _categoryProcessor;

	public SentryOptionsConfiguration(
		SentryExceptionsCategoryProcessor categoryProcessor)
	{
		_categoryProcessor = categoryProcessor;
	}

	public void Configure(SentryAspNetCoreOptions options)
	{
		options.AddEventProcessor(_categoryProcessor);
	}

	public void Configure(SentryLoggingOptions options)
	{
		options.AddEventProcessor(_categoryProcessor);
	}
}