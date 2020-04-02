using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Cqrs.Mediator
{
	/// <summary>
	/// A mediator acts as a single point of entry for commands, notifications
	/// and query execution while providing advanced extensibility options
	/// through a middleware based pipeline.
	/// </summary>
	public sealed class Mediator : IMediator
	{
		/// <summary>
		/// The middlewares.
		/// </summary>
		private readonly IMiddleware[] _middlewares;

		/// <summary>
		/// Creates a mediator.
		/// </summary>
		/// <param name="serviceProvider">The mediator service provider.</param>
		/// <param name="middlewaresFactories">The mediator pipeline middlewares.</param>
		public Mediator(IMediatorServiceProvider serviceProvider)
		{
			ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

			// The middlewares must be stored internally in reverse order
			// to ensure optimum performance and proper execution.
			_middlewares = serviceProvider.GetMiddlewares().Reverse().ToArray();
		}

		/// <summary>
		/// Gets the service provider.
		/// </summary>
		public IMediatorServiceProvider ServiceProvider { get; }

		/// <summary>
		/// Broadcasts a notification asynchronously by invoking the matching
		/// handlers as available in the service provider.
		/// </summary>
		/// <typeparam name="TNotification">The notification type.</typeparam>
		/// <param name="notification">The notification to broadcast.</param>
		/// <param name="cancellationToken">The asynchronous task cancellation token.</param>
		/// <returns>The asynchronous task.</returns>
		public Task BroadcastAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
			where TNotification : class, INotification
		{
			var handlers = ServiceProvider.GetServices<INotificationHandler<TNotification>>().ToArray();

			Func<TNotification, Task> next = (n) =>
			{
				var tasks = new List<Task>(handlers.Length);

				foreach (var handler in handlers)
					tasks.Add(handler.HandleNotificationAsync(n, cancellationToken));

				return Task.WhenAll(tasks);
			};

			foreach (var middleware in _middlewares)
			{
				var current = next;
				next = (n) => middleware.HandleNotificationAsync(n, current, cancellationToken);
			}

			return next(notification);
		}

		/// <summary>
		/// Executes a query asynchronously by invoking the matching
		/// handler as available in the service provider.
		/// </summary>
		/// <typeparam name="TQuery">The query type.</typeparam>
		/// <typeparam name="TQueryResult">The query result type.</typeparam>
		/// <param name="query">The query to execute.</param>
		/// <param name="cancellationToken">The asynchronous task cancellation token.</param>
		/// <exception cref="System.NotImplementedException">
		///	A matching query handler is not available through the service provider.
		/// </exception>
		/// <returns>The asynchronous task for the query result.</returns>
		public Task<TQueryResult> QueryAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken = default)
			where TQuery : class, IQuery<TQueryResult>
			where TQueryResult : class
		{
			var handler = ServiceProvider.GetQueryHandler<TQuery, TQueryResult>();

			Func<TQuery, Task<TQueryResult>> next = (q) =>
				handler.HandleQueryAsync(q, cancellationToken);

			foreach (var middleware in _middlewares)
			{
				var current = next;
				next = (q) => middleware.HandleQueryAsync(q, current, cancellationToken);
			}

			return next(query);
		}

		/// <summary>
		/// Executes a command asynchronously by invoking the matching
		/// handler as available in the service provider.
		/// </summary>
		/// <typeparam name="TCommand">The command type.</typeparam>
		/// <param name="command">The command to execute.</param>
		/// <param name="cancellationToken">The asynchronous task cancellation token.</param>
		/// <exception cref="System.NotImplementedException">
		///	A matching command handler is not available through the service provider.
		/// </exception>
		/// <returns>The asynchronous task.</returns>
		public Task RunAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
			where TCommand : class, ICommand
		{
			var handler = ServiceProvider.GetCommandHandler<TCommand>();

			Func<TCommand, Task> next = (c) =>
				handler.HandleCommandAsync(c, cancellationToken);

			foreach (var middleware in _middlewares)
			{
				var current = next;
				next = (c) => middleware.HandleCommandAsync(c, current, cancellationToken);
			}

			return next(command);
		}
	}
}
