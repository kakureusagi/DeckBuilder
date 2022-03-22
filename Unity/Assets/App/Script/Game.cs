using System.Linq;

namespace App
{
	public class Game
	{
		public Player Player => player;
		public Enemy[] Enemies => enemies;
		public Hand Hand => hand;

		readonly Player player;
		readonly Hand hand;
		readonly Enemy[] enemies;
		readonly DamageCalculator damageCalculator = new DamageCalculator();

		public Game()
		{
			player = new Player(80);
			enemies = new []{new Enemy(1, 100)};

			Card[] cards = new Card[5];
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

			hand = new Hand(cards);
		}

		public AttackPrediction PredictAttack(int enemyId)
		{
			return damageCalculator.Attack(enemyId);
		}

		public void AttackToEnemy(int enemyId)
		{
			var prediction = damageCalculator.Attack(enemyId);
			var enemy = enemies.First(x => x.Id == enemyId);
			enemy.AddHp(-prediction.Damage);
		}
	}
}