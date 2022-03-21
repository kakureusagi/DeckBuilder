using System.Collections.Generic;
using App.Input;
using App.State;
using UnityEngine;
using UnityEngine.UIElements;

namespace App
{
	public class GamePresenter : MonoBehaviour
	{
		[SerializeField]
		CardPresenter cardPrefab = default;

		[SerializeField]
		Hand hand = default;

		[SerializeField]
		EnemyPresenter enemy = default;

		[SerializeField]
		PlayerPresenter player = default;

		[SerializeField]
		Chain chain = default;

		[SerializeField]
		EnemyManager enemyManager = default;

		readonly List<CardPresenter> cards = new();

		Mouse mouse;
		CardPresenter currentCard;
		EnemyPresenter currentEnemyPresenter;

		Dictionary<State.State, IState> states;
		IState currentState;

		void Start()
		{
			mouse = new Mouse(Camera.main);
			chain.Hide();

			for (int i = 0; i < 5; i++)
			{
				var card = Instantiate(cardPrefab, null);
				hand.Add(card);
			}
			
			player.Initialize(new Player(80));

			enemy.Initialize(new Enemy(100));
			enemyManager.Initialize(new[] { enemy });

			states = new Dictionary<State.State, IState>
			{
				{ State.State.PlayerTurn, new PlayerState(mouse, chain, hand, player, enemyManager) },
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