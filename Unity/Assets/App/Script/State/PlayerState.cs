using System;
using App.Input;
using Cysharp.Threading.Tasks;
using UniRx;

namespace App.State
{
	public class PlayerState : IState
	{
		readonly Game game;
		readonly IMouse mouse;
		readonly Chain chain;
		readonly DeckPresenter deck;
		readonly PlayerPresenter player;
		readonly EnemyManager enemyManager;
		readonly DamageCalculator damageCalculator;
		readonly TurnEnd turnEnd;

		CardPresenter currentCard;
		EnemyPresenter currentEnemy;

		IDisposable turnEndClick;
		bool turnEnded;

		public PlayerState(Game game, IMouse mouse, Chain chain, DeckPresenter deck, PlayerPresenter player, EnemyManager enemyManager, DamageCalculator damageCalculator, TurnEnd turnEnd)
		{
			this.game = game;
			this.mouse = mouse;
			this.chain = chain;
			this.deck = deck;
			this.player = player;
			this.enemyManager = enemyManager;
			this.damageCalculator = damageCalculator;
			this.turnEnd = turnEnd;
		}

		public void OnEnter()
		{
			turnEnded = false;
			turnEndClick = turnEnd.OnClickAsObservable().Subscribe(_ =>
			{
				turnEnded = true;
				game.EndTurn();
			});
		}

		public void OnLeave()
		{
			turnEndClick.Dispose();
			turnEndClick = null;
		}

		public StateType Update()
		{
			if (turnEnded)
			{
				return StateType.EnemyTurn;
			}
			
			if (currentCard == null)
			{
				if (mouse.Button == MouseButton.Down)
				{
					if (deck.TryPick(mouse.Position, out var card))
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
					var prediction = damageCalculator.Attack(enemy.Enemy.Id);
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
						game.AttackToEnemy(currentEnemy.Enemy);
						game.Deck.Discard(currentCard.Card);
						
						currentEnemy.ResetPrediction();
						currentEnemy = null;
					}
					
					currentCard.UnSelect();
					currentCard = null;
					chain.Hide();
				}
			}

			return StateType.PlayerTurn;
		}
	}
}