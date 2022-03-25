using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App
{
	public class TurnEnd : MonoBehaviour, IPointerClickHandler
	{
		readonly Subject<Unit> onClick = new ();

		public void OnPointerClick(PointerEventData eventData)
		{
			onClick.OnNext(Unit.Default);
		}

		public IObservable<Unit> OnClickAsObservable()
		{
			return onClick;
		}
	}
}