using Microsoft.Extensions.DependencyInjection;
using System;

namespace Datapoint.Cqrs.Mediator.Extensions.Microsoft.DependencyInjection
{
	/// <summary>
	/// A set of mediator options.
	/// </summary>
	public sealed class MediatorOptions
	{
		/// <summary>
		/// The service collection.
		/// </summary>
		private readonly IServiceCollection _serviceCollection;

		/// <summary>
		/// Creates the mediator options.
		/// </summary>
		/// <param name="services">The service collection.</param>
		internal MediatorOptions(IServiceCollection serviceCollection)
		{
			_serviceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
		}

		/// <summary>
		///	Adds a scoped middleware service.
		/// </summary>
		/// <typeparam name="TMiddleware">The middleware type.</typeparam>
		/// <returns>This instance.</returns>
		public MediatorOptions AddMiddleware<TMiddleware>()
			where TMiddleware : class, IMiddleware
		{
			_serviceCollection.AddScoped<IMiddleware, TMiddleware>();
			return this;
		}
	}
}
