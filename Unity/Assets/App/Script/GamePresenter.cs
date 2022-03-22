using System.Collections.Generic;
using App.Input;
using App.State;
using UnityEngine;

namespace App
{
	public class GamePresenter : MonoBehaviour
	{
		[SerializeField]
		CardPresenter cardPrefab = default;

		[SerializeField]
		HandPresenter hand = default;

		[SerializeField]
		EnemyPresenter enemy = default;

		[SerializeField]
		PlayerPresenter player = default;

		[SerializeField]
		Chain chain = default;

		[SerializeField]
		EnemyManager enemyManager = default;

		readonly List<CardPresenter> cards = new();

		Game game;
		Mouse mouse;
		CardPresenter currentCard;
		EnemyPresenter currentEnemyPresenter;

		Dictionary<State.State, IState> states;
		IState currentState;

		void Start()
		{
			// 雑にここで開始する
			game = new Game();
			
			mouse = new Mouse(Camera.main);
			chain.Hide();

			foreach (var card in game.Hand.Cards)
			{
				var cardPresenter = Instantiate(cardPrefab, null);
				cardPresenter.Initialize(card);
				hand.Add(cardPresenter);
			}
			
			player.Initialize(game.Player);

			enemy.Initialize(game.Enemies[0]);
			enemyManager.Initialize(new[] { enemy });

			states = new Dictionary<State.State, IState>
			{
				{ State.State.PlayerTurn, new PlayerState(game, mouse, chain, hand, player, enemyManager, new DamageCalculator()) },
			};

			currentState = states[State.State.PlayerTurn];
		}

		public void Update()
		{
			mouse.Update();
			var nextState = currentState.Update();
			if (nextState != currentState)
			{
				currentState = nextState;
			}
		}
	}
}