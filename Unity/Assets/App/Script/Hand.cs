using System.Collections.Generic;

namespace App
{
	public class Hand
	{
		public IReadOnlyList<Card> Cards => cards;

		readonly List<Card> cards;
		
		public Hand(Card[] cards)
		{
			this.cards = new List<Card>(cards);
		}
		
		
	}
}