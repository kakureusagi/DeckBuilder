using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0108, CS0114

namespace App
{
	public class EnemyManager : MonoBehaviour
	{
		public IReadOnlyList<EnemyPresenter> Enemies => enemies;

		[SerializeField]
		Camera camera = default;
		
		EnemyPresenter[] enemies;
		
		public void Initialize(EnemyPresenter[] enemies)
		{
			this.enemies = enemies;
		}

		public bool TryGetEnemy(Vector3 mousePosition, out EnemyPresenter result)
		{
			result = null;
			var ray = camera.ScreenPointToRay(mousePosition);
			var hit = Physics2D.Raycast(ray.origin, ray.direction);
			if (hit.collider == null)
			{
				return false;
			}
            
			foreach (var enemy in enemies)
			{
				if (enemy.Collider == hit.collider)
				{
					result = enemy;
					return true;
				}
			}

			return false;
		}
	}
}