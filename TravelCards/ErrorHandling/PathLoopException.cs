using System;

namespace TravelCards.ErrorHandling
{
	/// <summary>
	/// Маршрут зациклился
	/// </summary>
	public class PathLoopException : Exception
	{
		public PathLoopException()
			: base("Маршрут зациклен.")
		{
		}
	}
}