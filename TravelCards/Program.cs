using System;
using System.Collections.Generic;
using System.Linq;
using TravelCards.ErrorHandling;

namespace TravelCards
{
	class Program
	{
		public static void Main(string[] args)
		{
			var unsortedCards = new List<TravelCard>();
			unsortedCards.Add(new TravelCard("Владивосток", "Гановер"));
			unsortedCards.Add(new TravelCard("Бостон", "Владивосток"));
			unsortedCards.Add(new TravelCard("Гановер", "Дублин"));
			unsortedCards.Add(new TravelCard("Ачинск", "Бостон"));
			unsortedCards.Add(new TravelCard("Дублин", "Екатеринбург"));

			try
			{
				var sortedCards = GetTravelCardChain(unsortedCards);
				foreach (var card in sortedCards)
				{
					Console.WriteLine(card);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			Console.ReadLine();
		}

		internal static List<TravelCard> GetTravelCardChain(List<TravelCard> source)
		{
			if (!(source != null && source.Count != 0))
			{
				throw new ArgumentException("Список карточек не может быть пустым");
			}

			// словарь городов, ключ - город, значение- карточка перехода к следующему городу
			var citiesFrom = new Dictionary<string, TravelCard>();

			// данный список нужен для определения города - начала маршрута, его не будет в данном списке
			var citiesTo = new List<string>();

			// если набор карточек корректен, для каждого ключа будет по одному значению
			foreach (var card in source)
			{
				citiesTo.Add(card.CityTo);

				try
				{
					citiesFrom.Add(card.CityFrom, card);
				}
				catch (ArgumentException)
				{
					throw new PathHasBranchesException();
				}
			}

			// определение начального города маршрута
			// начальный город должен быть единственным
			var startCities = citiesFrom.Keys.Except(citiesTo).ToList();
			if (startCities.Count == 0)
			{
				throw new PathLoopException();
			}

			if (startCities.Count > 1)
			{
				throw new PathHasToManyStartCitiesException();
			}

			var res = new List<TravelCard>();
			Travel(startCities[0], citiesFrom, res);
			return res;
		}

		/// <summary>
		/// Рекурсивно ходим по словарю городов маршрута, читаем из найденной карточки следующий город
		/// </summary>
		/// <param name="cityFrom">Город, лоткуда едем</param>
		/// <param name="cities">Словарь городов по пути</param>
		/// <param name="chain">Результирующий упорядоченный список - маршрут</param>
		private static void Travel(string cityFrom, Dictionary<string, TravelCard> cities, List<TravelCard> chain)
		{
			TravelCard currentCard;
			// из конца маршрута пути не будет, выход
			if (cities.TryGetValue(cityFrom, out currentCard))
			{
				// цикл, не хорошо
				if (currentCard.Visited)
				{
					throw new PathLoopException();
				}
				
				// в маршрут попадает карточка с дорогой в следующий город
				chain.Add(currentCard);
				currentCard.Visited = true;

				// рекурсивно движемся по цепочке карточек с помощью словаря городов
				Travel(currentCard.CityTo, cities, chain);
			}
		}
	}
}