using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.Template;

public static class ServiceCollectionExtensions
{
	/// <summary>
	///     Добавляет фильтрацию sentry ивентов по <see cref="IExceptionCategory.LogLevel" />.
	///     Ивент в sentry не будет уходить,
	///     если его уровень ниже чем указанный уровень в <see cref="SentryOptions.MinimumEventLevel" />.
	/// </summary>
	/// <remarks>
	///     <see cref="SentryOptions.MinimumEventLevel" /> проставляется в конфигах в
	///     секции <see cref="SentryOptions.SectionName" />.
	///     Если в конфигах не проставлен <see cref="SentryOptions.MinimumEventLevel" />,
	///     то будет использоваться уровень <see cref="LogLevel.Error" />.
	/// </remarks>
	public static IServiceCollection AddSentryExceptionsCategoryProcessor(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services
			.AddOptions<SentryOptions>()
			.Bind(configuration.GetSection(SentryOptions.SectionName));

		services.AddSingleton<SentryExceptionsCategoryProcessor>();

		services.ConfigureOptions<SentryOptionsConfiguration>();

		return services;
	}
}