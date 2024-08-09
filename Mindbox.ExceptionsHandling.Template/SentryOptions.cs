using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.Template;

public record SentryOptions
{
	public const string SectionName = "Sentry";

	public LogLevel MinimumEventLevel { get; set; } = LogLevel.Error;
}