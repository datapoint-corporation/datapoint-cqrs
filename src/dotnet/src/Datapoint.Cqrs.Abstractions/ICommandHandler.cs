using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Cqrs
{
	/// <summary>
	/// An asynchronous command handler.
	/// 
	/// A command makes changes to the system but does not produce a result. It
	/// can broadcast notifications which may result in further changes.
	/// </summary>
	/// <typeparam name="TCommand">The command type.</typeparam>
	public interface ICommandHandler<TCommand> where TCommand : class, ICommand
	{
		/// <summary>
		/// Handles a command asynchronously.
		/// 
		/// A command makes changes to the system but does not produce a result. It
		/// can broadcast notifications which may result in further changes.
		/// </summary>
		/// <param name="command">The command to handle.</param>
		/// <param name="cancellationToken">The asynchronous task cancellation token.</param>
		/// <returns>The asynchronous task.</returns>
		Task HandleCommandAsync(TCommand command, CancellationToken cancellationToken = default);
	}
}
