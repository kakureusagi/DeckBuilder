using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;

namespace App
{
	public class EnemyPresenter : MonoBehaviour
	{
		public Collider2D Collider => collider;
		
		
		[SerializeField]
		TMP_Text hp = default;

		[SerializeField]
		Collider2D collider = default;

		[SerializeField]
		Animator animator = default;

		Enemy enemy;
		UniTaskCompletionSource animationTask;

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
			hp.text = $"{enemy.Hp.Value}/{enemy.MaxHp.Value}";
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