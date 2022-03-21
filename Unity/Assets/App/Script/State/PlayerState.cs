using App.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.State
{
	public class PlayerState : IState
	{
		readonly IMouse mouse;
		readonly Chain chain;
		readonly Hand hand;
		readonly PlayerPresenter player;
		readonly EnemyManager enemyManager;

		CardPresenter currentCard;

		public PlayerState(IMouse mouse, Chain chain, Hand hand, PlayerPresenter player, EnemyManager enemyManager)
		{
			this.mouse = mouse;
			this.chain = chain;
			this.hand = hand;
			this.player = player;
			this.enemyManager = enemyManager;
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
				if (currentCard != null)
				{
					chain.Set(currentCard.transform.position, mouse.ScreenPosition);
					if (mouse.Button == MouseButton.Up)
					{
						if (enemyManager.TryPick(mouse.Position, out var enemy))
						{
							if (enemy != null)
							{
								player.Attack().Forget();
							}
						
							currentCard.UnSelect();
							currentCard = null;
							chain.Hide();
						}
					}
					
				}
				// if (mouse.Button == MouseButton.Up)
				// {
				// 	if (currentCard != null)
				// 	{
				// 		currentCard.UnSelect();
				// 		currentCard = null;
				// 		chain.Hide();
				// 	}
				// }
				// else
				// {
				// 	enemyManager.TryPick(mouse.Position, out var enemy);
				// 	if (enemy != null)
				// 	{
				// 		if (currentEnemy != enemy)
				// 		{
				// 			if (currentEnemy != null)
				// 			{
				// 				currentEnemy.UnSelect();
				// 			}
				// 			currentEnemy = enemy;
				// 			currentEnemy.Select();
				// 		}
				// 	}
				// 	else
				// 	{
				// 		if (currentEnemy != null)
				// 		{
				// 			currentEnemy.UnSelect();
				// 			currentEnemy = null;
				// 		}
				// 	}
				// }
			}

			return this;
		}
	}
}