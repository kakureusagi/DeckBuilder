using Cysharp.Threading.Tasks;

namespace App.State
{
	public class EnemyState : IState
	{
		readonly Game game;
		readonly EnemyManager enemyManager;
		
		bool hasAnimationStarted;
		bool hasAnimationFinished;

		public EnemyState(Game game, EnemyManager enemyManager)
		{
			this.game = game;
			this.enemyManager = enemyManager;
		}

		public void OnEnter()
		{
			hasAnimationStarted = false;
			hasAnimationFinished = false;
		}

		public void OnLeave()
		{
		}

		public StateType Update()
		{
			if (!hasAnimationStarted)
			{
				hasAnimationStarted = true;
				PlayAnimations().Forget();
			}
			
			return hasAnimationFinished ? StateType.PlayerTurn : StateType.EnemyTurn;
		}

		async UniTask PlayAnimations()
		{
			foreach (var presenter in enemyManager.Enemies)
			{
				game.AttackToPlayer(presenter.Enemy);
				await presenter.Attack();
			}

			hasAnimationFinished = true;
		}
	}
}