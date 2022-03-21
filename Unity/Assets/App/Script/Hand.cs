using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class Hand : MonoBehaviour
    {

        [SerializeField]
        Camera camera = default;

        [SerializeField]
        Transform root = default;

        readonly List<CardPresenter> cards = new List<CardPresenter>();

        public void Add(CardPresenter card)
        {
            cards.Add(card);
            card.transform.SetParent(root);
            
            AdjustCards();
        }

        public bool TryPick(Vector3 position, out CardPresenter result)
        {
            result = null;
            var ray = camera.ScreenPointToRay(position);
            var hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider == null)
            {
                return false;
            }
            
            foreach (var card in cards)
            {
                if (card.Collider == hit.collider)
                {
                    result = card;
                    return true;
                }
            }

            return false;
        }

        void AdjustCards()
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var startPosition = -(cards.Count - 1) / 2f * CardPresenter.Width * 1.1f;
                var x = startPosition + CardPresenter.Width * 1.1f * i;
                card.transform.localPosition = new Vector3(x, 0, 0);
            }
        }
    }
}
