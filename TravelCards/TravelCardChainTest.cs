using System;
using System.Collections.Generic;

using NUnit.Framework;

using TravelCards.ErrorHandling;

namespace TravelCards
{
	[TestFixture]
	public class TravelCardChainTest
	{
		private List<TravelCard> _unsortedCards;
		private List<TravelCard> _sortedCards;

		[SetUp]
		[TearDown]
		public void Init()
		{
			_unsortedCards = new List<TravelCard>
								{
									new TravelCard("Владивосток", "Гановер"),
									new TravelCard("Бостон", "Владивосток"),
									new TravelCard("Гановер", "Дублин"),
									new TravelCard("Ачинск", "Бостон"),
									new TravelCard("Дублин", "Екатеринбург")
								};

			_sortedCards = new List<TravelCard>
								{
									new TravelCard("Ачинск", "Бостон"),
									new TravelCard("Бостон", "Владивосток"),
									new TravelCard("Владивосток", "Гановер"),
									new TravelCard("Гановер", "Дублин"),
									new TravelCard("Дублин", "Екатеринбург")
								};

		}

		[Test]
		public void TravelCard_ConstructFailed()
		{
			Assert.Throws<ArgumentException>(() =>new TravelCard(null, "1"));
			Assert.Throws<ArgumentException>(() => new TravelCard("1", string.Empty));
		}

		[Test]
		public void GetTravelCardChain_Ok()
		{
			var res = Program.GetTravelCardChain(this._unsortedCards);
			Assert.AreEqual(string.Join(";", this._sortedCards), string.Join(";", res));

			// вырождение
			var twoCities = new List<TravelCard> { new TravelCard("Москва", "Рязань") };
			res = Program.GetTravelCardChain(twoCities);
			Assert.AreEqual(string.Join(";", twoCities), string.Join(";", res));
		}

		[Test]
		public void GetTravelCardChain_Failed_EmptySource()
		{
			Assert.Throws<ArgumentException>(() => Program.GetTravelCardChain(null));
			Assert.Throws<ArgumentException>(() => Program.GetTravelCardChain(new List<TravelCard>()));
		}

		[Test]
		public void GetTravelCardChain_Failed_LoopEndStart()
		{
			// цикл конец-начало
			this._unsortedCards.Add(new TravelCard("Екатеринбург", "Ачинск"));
			Assert.Throws<PathLoopException>(() => Program.GetTravelCardChain(this._unsortedCards));
		}

		[Test]
		public void GetTravelCardChain_Failed_InnerLoopt()
		{
			// внутренний цикл
			this._unsortedCards.RemoveAt(this._unsortedCards.Count - 1);
			this._unsortedCards.Add(new TravelCard("Дублин", "Владивосток"));
			Assert.Throws<PathLoopException>(() => Program.GetTravelCardChain(this._unsortedCards));
		}

		[Test]
		public void GetTravelCardChain_Failed_NoStartCity()
		{
			this._unsortedCards.Add(new TravelCard("Торонто", "Брест"));
			Assert.Throws<PathHasToManyStartCitiesException>(() => Program.GetTravelCardChain(this._unsortedCards));
		}

		[Test]
		public void GetTravelCardChain_Failed_Branhces()
		{
			this._unsortedCards.Add(new TravelCard("Владивосток", "Барселона"));
			Assert.Throws<PathHasBranchesException>(() => Program.GetTravelCardChain(this._unsortedCards));
		}
	}
}