using System.Collections.Generic;

namespace Datapoint.Cqrs.Mediator
{
	/// <summary>
	/// A provider for mediator services such as command handles, notification
	/// handlers, query handlers and others.
	/// </summary>
	public interface IMediatorServiceProvider
	{
		/// <summary>
		/// Gets a command handlers.
		/// </summary>
		/// <typeparam name="TCommand">The command type.</typeparam>
		/// <returns>The command handler, if available.</returns>
		ICommandHandler<TCommand> GetCommandHandler<TCommand>()
			where TCommand : class, ICommand;

		/// <summary>
		/// Gets the middlewares.
		/// </summary>
		/// <returns>The middlewares.</returns>
		IEnumerable<IMiddleware> GetMiddlewares();

		/// <summary>
		/// Gets a notification handlers.
		/// </summary>
		/// <typeparam name="TNotification">The notification type.</typeparam>
		/// <returns>The notification handlers.</returns>
		IEnumerable<INotificationHandler<TNotification>> GetNotificationHandlers<TNotification>()
			where TNotification : class, INotification;

		/// <summary>
		/// Gets a query handler.
		/// </summary>
		/// <typeparam name="TQuery">The query type.</typeparam>
		/// <typeparam name="TQueryResult">The query result.</typeparam>
		/// <returns>The query handler, if available.</returns>
		IQueryHandler<TQuery, TQueryResult> GetQueryHandler<TQuery, TQueryResult>()
			where TQuery : class, IQuery<TQueryResult>
			where TQueryResult : class;

		/// <summary>
		/// Gets a generic service.
		/// </summary>
		/// <typeparam name="TService">The generic service type.</typeparam>
		/// <returns>The generic service, if available.</returns>
		TService GetService<TService>()
			where TService : class;

		/// <summary>
		/// Gets generic services.
		/// </summary>
		/// <typeparam name="TService">The generic service type.</typeparam>
		/// <returns>The generic services.</returns>
		IEnumerable<TService> GetServices<TService>()
			where TService : class;
	}
}
