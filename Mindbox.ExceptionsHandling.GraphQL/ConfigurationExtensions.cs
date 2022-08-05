using Microsoft.Extensions.DependencyInjection;

namespace Mindbox.ExceptionsHandling.GraphQL;

public static class ConfigurationExtensions
{
	public static IServiceCollection AddGraphQLErrorHandling(this IServiceCollection services) =>
		services
			.AddErrorFilter<GraphQLHandleErrorFilter>()
			.AddHttpResultSerializer<GraphQLHttpAgnosticResultSerializer>();
}