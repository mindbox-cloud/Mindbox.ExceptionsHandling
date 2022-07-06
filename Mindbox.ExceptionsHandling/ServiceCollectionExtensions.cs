using System;

using Microsoft.Extensions.DependencyInjection;

namespace Mindbox.ExceptionsHandling;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddSystemExceptionCategories(this IServiceCollection services) =>
		services
			.AddExceptionCategory(
				new UnauthorizedAccessExceptionCategory(exception => exception is UnauthorizedAccessException))
			.AddExceptionCategory(
				new ServiceUnavailableExceptionCategory(exception => exception is TimeoutException));
}