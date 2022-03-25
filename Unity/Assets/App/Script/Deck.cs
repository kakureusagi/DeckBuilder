using System;
using System.Linq;
using UniRx;

namespace App
{
	public class Deck
	{
		public IReadOnlyReactiveCollection<Card> DrawPile => drawPile;
		public IReadOnlyReactiveCollection<Card> Hand => hand;
		public IReadOnlyReactiveCollection<Card> Discards => discards;

		readonly ReactiveCollection<Card> drawPile;
		readonly ReactiveCollection<Card> hand = new ();
		readonly ReactiveCollection<Card> discards = new ();

		
		public Deck(Card[] cards)
		{
			drawPile = new (cards);
		}

		public void Discard(Card card)
		{
			if (hand.All(x => x.Id != card.Id))
			{
				throw new ArgumentOutOfRangeException(nameof(card));
			}

			hand.Remove(card);
			discards.Add(card);
		}

		public void Draw(int number)
		{
			if (drawPile.Count >= number)
			{
				for (var i = 0; i < number; i++)
				{
					hand.Add(drawPile[0]);
					drawPile.RemoveAt(0);
				}
			}
			else
			{
				var leftSize = drawPile.Count;
				for (var i = 0; i < leftSize; i++)
				{
					hand.Add(drawPile[0]);
					drawPile.RemoveAt(0);
				}
				
				foreach (var card in discards)
				{
					drawPile.Add(card);
				}
				discards.Clear();

				var diff = number - leftSize;
				for (var i = 0; i < diff; i++)
				{
					hand.Add(drawPile[0]);
					drawPile.RemoveAt(0);
				}
			}
		}
	}
}