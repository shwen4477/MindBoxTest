using System;

namespace TravelCards
{
	/// <summary>
	/// Модель карточки
	/// </summary>
	public class TravelCard
	{
		/// <summary>
		/// Откуда
		/// </summary>
		public readonly string CityFrom;

		/// <summary>
		/// Куда
		/// </summary>
		public readonly string CityTo;

		public TravelCard(string cityFrom, string cityTo)
		{
			if (string.IsNullOrWhiteSpace(cityFrom) || string.IsNullOrWhiteSpace(cityTo))
			{
				throw new ArgumentException("Название города не может быть пустым");
			}

			this.CityFrom = cityFrom;
			this.CityTo = cityTo;
		}

		/// <summary>
		/// Данная карточуа уже имеется в маршруте
		/// </summary>
		public bool Visited { get; set; }

		public override string ToString()
		{
			return $"{this.CityFrom} - {this.CityTo}";
		}
	}
}