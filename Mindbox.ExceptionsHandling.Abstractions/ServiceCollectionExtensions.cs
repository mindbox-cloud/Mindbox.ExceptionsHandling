using Microsoft.Extensions.DependencyInjection;

namespace Mindbox.ExceptionsHandling
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddExceptionCategory(
			this IServiceCollection services, IExceptionCategory exceptionCategory) => services.AddSingleton(exceptionCategory);
	}
}