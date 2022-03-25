using System.Collections.Generic;
using App.Input;
using App.State;
using UnityEngine;

namespace App
{
	public class GamePresenter : MonoBehaviour
	{
		[SerializeField]
		DeckPresenter deck = default;

		[SerializeField]
		EnemyPresenter enemy = default;

		[SerializeField]
		PlayerPresenter player = default;

		[SerializeField]
		Chain chain = default;

		[SerializeField]
		EnemyManager enemyManager = default;

		[SerializeField]
		TurnEnd turnEnd = default;

		readonly List<CardPresenter> cards = new();

		Game game;
		Mouse mouse;
		CardPresenter currentCard;
		EnemyPresenter currentEnemyPresenter;

		Dictionary<StateType, IState> states;
		IState currentState;
		StateType currentStateType;

		void Start()
		{
			// 雑にここで開始する
			game = new Game();

			mouse = new Mouse(Camera.main);
			chain.Hide();

			deck.Initialize(game.Deck);

			player.Initialize(game.Player);

			enemy.Initialize(game.Enemies[0]);
			enemyManager.Initialize(new[] { enemy });

			states = new Dictionary<StateType, IState>
			{
				{ StateType.PlayerTurn, new PlayerState(game, mouse, chain, deck, player, enemyManager, new DamageCalculator(), turnEnd) },
				{ StateType.None, new NoneState() },
				{ StateType.EnemyTurn, new EnemyState(game, enemyManager) },
			};

			currentStateType = StateType.None;
			currentState = new NoneState();
		}

		public void Update()
		{
			mouse.Update();

			if (currentStateType == StateType.None)
			{
				currentState = states[StateType.PlayerTurn];
				currentState.OnEnter();
			}

			var nextStateType = currentState.Update();
			if (nextStateType != currentStateType)
			{
				currentStateType = nextStateType;
				currentState.OnLeave();
				currentState = states[currentStateType];
				currentState.OnEnter();
			}
		}
	}
}