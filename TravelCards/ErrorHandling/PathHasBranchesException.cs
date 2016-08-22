using System;

namespace TravelCards.ErrorHandling
{
	/// <summary>
	/// В маршруте есть ветвление
	/// </summary>
	public class PathHasBranchesException : Exception
	{
		public PathHasBranchesException()
			: base("Из одного города строятся два маршрута, это не правильно.")
		{
		}
	}
}