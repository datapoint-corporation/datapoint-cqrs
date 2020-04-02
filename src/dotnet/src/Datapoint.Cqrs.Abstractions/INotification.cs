using System;

namespace Datapoint.Cqrs
{
	/// <summary>
	/// A notification can result in the execution of further commands or queries
	/// and, as a result, may cause indirect changes to the system. It, however,
	/// does not produce a result.
	/// </summary>
	public interface INotification
	{
		/// <summary>
		/// Gets the global unique identifier.
		/// </summary>
		Guid Guid { get; }
	}
}
