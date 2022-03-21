using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace App
{
	public class Chain : MonoBehaviour
	{
		[SerializeField]
		SpriteRenderer[] parts = default;

		[SerializeField]
		Color startColor = default;

		[SerializeField]
		Color endColor = default;

		public void Set(Vector2 start, Vector2 end)
		{
			for (var i = 0; i < parts.Length; i++)
			{
				parts[i].transform.localPosition = Bezier(start, end, (float)i / (parts.Length - 1));
			}
		}

		private static Vector2 Bezier(Vector2 start, Vector2 end, float time)
		{
			var middle = new Vector2(start.x, end.y);
			var v1 = Vector2.Lerp(start, middle, time);
			var v2 = Vector2.Lerp(middle, end, time);
			return Vector2.Lerp(v1, v2, time);
		}

		public void Show()
		{
			foreach (var part in parts)
			{
				part.gameObject.SetActive(true);
			}
		}

		public void Hide()
		{
			foreach (var part in parts)
			{
				part.gameObject.SetActive(false);
			}
		}

		[ContextMenu("Reset Chain")]
		private void ResetChain()
		{
			for (var i = 0; i < parts.Length; i++)
			{
				var renderer = parts[i];
				renderer.color = startColor + (endColor - startColor) * ((float)i / (parts.Length - 1));
				renderer.sortingOrder = 110;
				renderer.transform.localPosition = new Vector3(i * 0.3f, 0, -i * 0.001f);
				renderer.transform.localScale = Vector3.one * 0.7f;
			}
		}
	}
}