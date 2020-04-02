using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Datapoint.Cqrs.Mediator.Extensions.Microsoft.DependencyInjection
{
	/// <summary>
	/// A mediator service provider that wraps the Microsoft default
	/// service provider implementation.
	/// </summary>
	public sealed class MicrosoftMediatorServiceProvider : MediatorServiceProvider
	{
		/// <summary>
		/// The service provider.
		/// </summary>
		private readonly IServiceProvider _serviceProvider;

		/// <summary>
		/// Creates a Microsoft mediator service provider.
		/// </summary>
		/// <param name="serviceProvider">The microsoft service provider.</param>
		public MicrosoftMediatorServiceProvider(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		}

		/// <summary>
		/// Gets a generic service.
		/// </summary>
		/// <typeparam name="TService">The generic service type.</typeparam>
		/// <returns>The generic service, if available.</returns>
		public override TService GetService<TService>() =>
			_serviceProvider.GetService<TService>();

		/// <summary>
		/// Gets generic services.
		/// </summary>
		/// <typeparam name="TService">The generic service type.</typeparam>
		/// <returns>The generic services.</returns>
		public override IEnumerable<TService> GetServices<TService>() =>
			_serviceProvider.GetServices<TService>();
	}
}
