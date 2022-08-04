using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling.GraphQL;

public static class ConfigurationExtensions
{
	public static IRequestExecutorBuilder AddExceptionsHandling(this IRequestExecutorBuilder requestBuilder)
	{
		requestBuilder
			.AddErrorFilter(
				serviceProvider => new GraphQLLogErrorFilter(
					serviceProvider.GetApplicationService<IExceptionCategoryMatcher>(),
					serviceProvider.GetApplicationService<ILoggerFactory>()));

		return requestBuilder;
	}

	public static IServiceCollection AddGraphQLStatusCodeHandling(this IServiceCollection services) =>
		services.AddHttpResultSerializer<GraphQLHttpAgnosticResultSerializer>();
}