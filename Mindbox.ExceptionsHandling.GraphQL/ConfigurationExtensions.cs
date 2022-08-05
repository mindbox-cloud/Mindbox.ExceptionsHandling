using Microsoft.Extensions.DependencyInjection;

namespace Mindbox.ExceptionsHandling.GraphQL;

public static class ConfigurationExtensions
{
	/// <summary>
	/// Must be called after registering all error filters in order for logging suppression to work.
	/// </summary>
	public static IServiceCollection AddGraphQLErrorHandling(this IServiceCollection services) =>
		services
			.AddErrorFilter<GraphQLHandleErrorFilter>()
			.AddHttpResultSerializer<GraphQLHttpAgnosticResultSerializer>();
}