using App.Input;
using Cysharp.Threading.Tasks;

namespace App.State
{
	public class PlayerState : IState
	{
		readonly Game game;
		readonly IMouse mouse;
		readonly Chain chain;
		readonly Hand hand;
		readonly PlayerPresenter player;
		readonly EnemyManager enemyManager;
		readonly DamageCalculator damageCalculator;

		CardPresenter currentCard;
		EnemyPresenter currentEnemy;

		public PlayerState(Game game, IMouse mouse, Chain chain, Hand hand, PlayerPresenter player, EnemyManager enemyManager, DamageCalculator damageCalculator)
		{
			this.game = game;
			this.mouse = mouse;
			this.chain = chain;
			this.hand = hand;
			this.player = player;
			this.enemyManager = enemyManager;
			this.damageCalculator = damageCalculator;
		}

		public IState Update()
		{
			if (currentCard == null)
			{
				if (mouse.Button == MouseButton.Down)
				{
					if (hand.TryPick(mouse.Position, out var card))
					{
						currentCard = card;
						currentCard.Select();
						chain.Set(currentCard.transform.position, mouse.ScreenPosition);
						chain.Show();
					}
				}
			}
			else
			{
				chain.Set(currentCard.transform.position, mouse.ScreenPosition);

				if (enemyManager.TryGetEnemy(mouse.Position, out var enemy))
				{
					currentEnemy = enemy;
					var prediction = damageCalculator.Attack(enemy.Id);
					enemy.SetPrediction(prediction);
				}
				else
				{
					if (currentEnemy != null)
					{
						currentEnemy.ResetPrediction();
						currentEnemy = null;
					}
				}

				if (mouse.Button == MouseButton.Up)
				{
					if (currentEnemy != null)
					{
						player.Attack().Forget();
						game.AttackToEnemy(currentEnemy.Id);
						
						currentEnemy.ResetPrediction();
						currentEnemy = null;
					}
					
					currentCard.UnSelect();
					currentCard = null;
					chain.Hide();
				}
			}

			return this;
		}
	}
}