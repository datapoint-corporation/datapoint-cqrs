using System;

namespace Datapoint.Cqrs
{
	/// <summary>
	/// A query does not change but reads the system state to produce a result.
	/// It can broadcast notifications which may result in indirect changes.
	/// </summary>
	/// <typeparam name="TQueryResult">The query result type.</typeparam>
	public interface IQuery<TQueryResult> where TQueryResult : class
	{
		/// <summary>
		/// Gets the global unique identifier.
		/// </summary>
		Guid Guid { get; }
	}
}
