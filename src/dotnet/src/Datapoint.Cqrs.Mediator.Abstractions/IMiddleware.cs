using System;
using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Cqrs.Mediator
{
	/// <summary>
	/// A middleware is used to modify the execution pipeline for commands,
	/// notifications and queries. It may use or change whatever goes through
	/// the mediator pipeline, including query results.
	/// </summary>
	public interface IMiddleware
	{
		/// <summary>
		/// Handles a command asynchronously.
		/// 
		/// A command makes changes to the system but does not produce a result. It
		/// can broadcast notifications which may result in further changes.
		/// </summary>
		/// <param name="command">The command to handle.</param>
		/// <param name="next">The next middleware function.</param>
		/// <param name="cancellationToken">The asynchronous task cancellation token.</param>
		/// <returns>The asynchronous task.</returns>
		Task HandleCommandAsync<TCommand>(TCommand command, Func<TCommand, Task> next, CancellationToken cancellationToken = default)
			where TCommand : class, ICommand;

		/// <summary>
		/// Handles a notification asynchronously.
		/// </summary>
		/// <param name="notification">The notification to handle.</param>
		/// <param name="next">The next middleware function.</param>
		/// <param name="cancellationToken">The asynchronous task cancellation token.</param>
		/// <returns>The asynchronous task.</returns>
		Task HandleNotificationAsync<TNotification>(TNotification notification, Func<TNotification, Task> next, CancellationToken cancellationToken = default)
			where TNotification : class, INotification;

		/// <summary>
		/// Handles a query asynchronously.
		/// 
		/// A query does not change but reads the system state to produce a result.
		/// It can broadcast notifications which may result in indirect changes.
		/// </summary>
		/// <param name="query">The query to handle.</param>
		/// <param name="next">The next middleware function.</param>
		/// <param name="cancellationToken">The asynchronous task cancellation token.</param>
		/// <returns>The asynchronous task for the query result.</returns>
		Task<TQueryResult> HandleQueryAsync<TQuery, TQueryResult>(TQuery query, Func<TQuery, Task<TQueryResult>> next, CancellationToken cancellationToken = default)
			where TQuery : class, IQuery<TQueryResult>
			where TQueryResult : class;
	}
}
