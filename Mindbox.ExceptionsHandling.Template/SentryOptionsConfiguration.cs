using Microsoft.Extensions.Options;
using Sentry;
using Sentry.AspNetCore;
using Sentry.Extensions.Logging;
using Sentry.NLog;

namespace Mindbox.ExceptionsHandling.Template;

internal class SentryOptionsConfiguration : IConfigureOptions<SentryAspNetCoreOptions>, IConfigureOptions<SentryLoggingOptions>,
	IConfigureNamedOptions<SentryNLogOptions>
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

	public void Configure(SentryNLogOptions options)
	{
		Configure("autobugsSentry_wrapped", options);
	}

	public void Configure(string? name, SentryNLogOptions options)
	{
		options.AddEventProcessor(_categoryProcessor);
	}
}