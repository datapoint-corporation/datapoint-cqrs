using System;

namespace Datapoint.Cqrs
{
	/// <summary>
	/// A command makes changes to the system but does not produce a result. It
	/// can broadcast notifications which may result in further changes.
	/// </summary>
	public interface ICommand
	{
		/// <summary>
		/// Gets the global unique identifier.
		/// </summary>
		Guid Guid { get; }
	}
}
