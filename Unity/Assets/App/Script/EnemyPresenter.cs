using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;

namespace App
{
	public class EnemyPresenter : MonoBehaviour
	{
		public int Id => enemy.Id;
		public Collider2D Collider => collider;
		
		
		[SerializeField]
		TMP_Text hp = default;

		[SerializeField]
		Collider2D collider = default;

		[SerializeField]
		Animator animator = default;

		Enemy enemy;
		UniTaskCompletionSource animationTask;

		AttackPrediction attackPrediction;

		public void Initialize(Enemy enemy)
		{
			this.enemy = enemy;

			enemy.Hp.Merge(enemy.MaxHp)
				.Subscribe(_ => UpdateHp())
				.AddTo(this);
		}

		public async UniTask Attack()
		{
			animator.CrossFade("Attack_001", 0.1f);
			animationTask = new UniTaskCompletionSource();
			await animationTask.Task;
			animationTask = null;
		}

		public void SetPrediction(AttackPrediction prediction)
		{
			attackPrediction = prediction;
			UpdateHp();
		}

		public void ResetPrediction()
		{
			attackPrediction = null;
			UpdateHp();
		}

		public void Anim_OnAttackFinish()
		{
			if (animationTask == null)
			{
				return;
			}
            
			animationTask.TrySetResult();
		}

		void UpdateHp()
		{
			if (attackPrediction == null)
			{
				hp.text = $"{enemy.Hp.Value}/{enemy.MaxHp.Value}";
			}
			else
			{
				hp.text = $"<color=#FF4444>{enemy.Hp.Value - attackPrediction.Damage}</color>/{enemy.MaxHp.Value}";
			}
		}

		public void Select()
		{
			Debug.Log("selected");
		}

		public void UnSelect()
		{
			Debug.Log("un selected");
		}
	}
}