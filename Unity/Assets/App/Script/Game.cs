using System.Linq;

namespace App
{
	public class Game
	{
		public Player Player => player;
		public Enemy[] Enemies => enemies;

		readonly Player player;
		readonly Enemy[] enemies;
		readonly DamageCalculator damageCalculator = new DamageCalculator();

		public Game()
		{
			player = new Player(80);
			enemies = new []{new Enemy(1, 100)};
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