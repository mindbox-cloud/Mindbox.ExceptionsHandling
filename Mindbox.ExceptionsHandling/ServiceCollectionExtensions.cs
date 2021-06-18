using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mindbox.ExceptionsHandling
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddExceptionCategory(
			this IServiceCollection services, IExceptionCategory exceptionCategory) => services.AddSingleton(exceptionCategory);

		public static IServiceCollection AddSystemExceptionCategories(this IServiceCollection services) => services
			.AddExceptionCategory(new UnauthorizedAccessExceptionCategory(exception => exception is UnauthorizedAccessException))
			.AddExceptionCategory(new ServiceUnavailableExceptionCategory(exception => exception is TimeoutException))
			.AddExceptionCategory(
					new ServiceUnavailableExceptionCategory(exception => exception is OutOfMemoryException, LogLevel.Critical));
	}
}