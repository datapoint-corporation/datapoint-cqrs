using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Cqrs
{
	/// <summary>
	/// An asynchronous notification handler.
	/// 
	/// A notification can result in the execution of further commands or queries
	/// and, as a result, may cause indirect changes to the system. It, however,
	/// does not produce a result.
	/// </summary>
	/// <typeparam name="TNotification">The notification type.</typeparam>
	public interface INotificationHandler<TNotification> where TNotification : class, INotification
	{
		/// <summary>
		/// Handles a notification asynchronously.
		/// </summary>
		/// <param name="notification">The notification to handle.</param>
		/// <param name="cancellationToken">The asynchronous task cancellation token.</param>
		/// <returns>The asynchronous task.</returns>
		Task HandleNotificationAsync(TNotification notification, CancellationToken cancellationToken = default);
	}
}
