using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Cqrs.Mediator
{
	/// <summary>
	/// A mediator acts as a single point of entry for commands, notifications
	/// and query execution while providing advanced extensibility options
	/// through a middleware based pipeline.
	/// </summary>
	public interface IMediator
	{
		/// <summary>
		/// Gets the service provider.
		/// </summary>
		IMediatorServiceProvider ServiceProvider { get; }

		/// <summary>
		/// Broadcasts a notification asynchronously by invoking the matching
		/// handlers as available in the service provider.
		/// </summary>
		/// <typeparam name="TNotification">The notification type.</typeparam>
		/// <param name="notification">The notification to broadcast.</param>
		/// <param name="cancellationToken">The asynchronous task cancellation token.</param>
		/// <returns>The asynchronous task.</returns>
		Task BroadcastAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
			where TNotification : class, INotification;

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
		Task<TQueryResult> QueryAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken = default)
			where TQuery : class, IQuery<TQueryResult>
			where TQueryResult : class;

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
		Task RunAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
			where TCommand : class, ICommand;
	}
}
