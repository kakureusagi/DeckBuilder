using System.Linq;

namespace App
{
	public class Game
	{
		public Player Player => player;
		public Enemy[] Enemies => enemies;
		public Deck Deck => deck;

		readonly Player player;
		readonly Deck deck;
		readonly Enemy[] enemies;
		readonly DamageCalculator damageCalculator = new ();

		public Game()
		{
			player = new Player(80);
			enemies = new[] { new Enemy(1, 100) };

			Card[] cards = new Card[20];
			for (int i = 0; i < cards.Length; i++)
			{
				cards[i] = new Card
				{
					Attack = 5,
					Block = 0,
					Cost = 1,
					Id = i + 1,
					Name = $"カード_{i + 1}",
				};
			}

			deck = new Deck(cards);
			deck.Draw(5);
		}

		public void EndTurn()
		{
			var temp = deck.Hand.ToArray();
			foreach (var card in temp)
			{
				deck.Discard(card);
			}

			deck.Draw(5);
		}

		public void AttackToEnemy(Enemy target)
		{
			var prediction = damageCalculator.Attack(target.Id);
			target.AddHp(-prediction.Damage);
		}

		public void AttackToPlayer(Enemy from)
		{
			player.AddHp(-15);
		}
	}
}