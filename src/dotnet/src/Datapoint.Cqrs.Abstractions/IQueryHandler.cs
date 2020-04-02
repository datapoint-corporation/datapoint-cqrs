using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Cqrs
{
	/// <summary>
	/// An asynchronous query handler.
	/// 
	/// A query does not change but reads the system state to produce a result.
	/// It can broadcast notifications which may result in indirect changes.
	/// </summary>
	/// <typeparam name="TQuery">The query type.</typeparam>
	/// <typeparam name="TQueryResult">The query result type.</typeparam>
	public interface IQueryHandler<TQuery, TQueryResult>
		where TQuery : class, IQuery<TQueryResult>
		where TQueryResult : class
	{
		/// <summary>
		/// Handles a query asynchronously.
		/// 
		/// A query does not change but reads the system state to produce a result.
		/// It can broadcast notifications which may result in indirect changes.
		/// </summary>
		/// <param name="query">The query to handle.</param>
		/// <param name="cancellationToken">The asynchronous task cancellation token.</param>
		/// <returns>The asynchronous task for the query result.</returns>
		Task<TQueryResult> HandleQueryAsync(TQuery query, CancellationToken cancellationToken = default);
	}
}
