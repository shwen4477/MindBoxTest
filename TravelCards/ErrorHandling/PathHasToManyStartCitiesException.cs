using System;

namespace TravelCards.ErrorHandling
{
	/// <summary>
	/// Начальных городов маршрута больше 1
	/// </summary>
	public class PathHasToManyStartCitiesException : Exception
	{
		public PathHasToManyStartCitiesException()
			: base("Не удалось определить стартовый город маршрута, таких городов больше 1.")
		{
		}
	}
}