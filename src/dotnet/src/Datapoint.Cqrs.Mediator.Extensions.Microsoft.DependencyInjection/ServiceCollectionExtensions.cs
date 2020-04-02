using Microsoft.Extensions.DependencyInjection;
using System;

namespace Datapoint.Cqrs.Mediator.Extensions.Microsoft.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds the Mediator as a scoped service.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddMediator(this IServiceCollection serviceCollection)
		{
			if (serviceCollection == null)
				throw new ArgumentNullException(nameof(serviceCollection));

			return serviceCollection
				.AddScoped<IMediatorServiceProvider, MicrosoftMediatorServiceProvider>()
				.AddScoped<IMediator, Mediator>();
		}

		/// <summary>
		/// Adds the Mediator as a scoped service.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		/// <param name="configuration">The mediator configuration action.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddMediator(this IServiceCollection serviceCollection, Action<MediatorOptions> configuration)
		{
			if (serviceCollection == null)
				throw new ArgumentNullException(nameof(serviceCollection));

			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			configuration(new MediatorOptions(serviceCollection));

			return serviceCollection
				.AddScoped<IMediatorServiceProvider, MicrosoftMediatorServiceProvider>()
				.AddScoped<IMediator, Mediator>();
		}
	}
}
