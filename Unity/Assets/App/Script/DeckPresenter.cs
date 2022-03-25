using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
#pragma warning disable CS0108, CS0114

namespace App
{
    public class DeckPresenter : MonoBehaviour
    {

        [SerializeField]
        Camera camera = default;

        [SerializeField]
        Transform root = default;
        
        [SerializeField]
        CardPresenter cardPrefab = default;

        Deck deck;
        readonly List<CardPresenter> cards = new List<CardPresenter>();

        public void Initialize(Deck deck)
        {
            this.deck = deck;
            
            foreach (var card in deck.Hand)
            {
                var cardPresenter = Instantiate(cardPrefab, root);
                cardPresenter.Initialize(card);

                cards.Add(cardPresenter);
                AdjustCards();
            }

            deck.Hand.ObserveAdd()
                .Subscribe(e =>
                {
                    var cardPresenter = Instantiate(cardPrefab, root);
                    cardPresenter.Initialize(e.Value);

                    cards.Add(cardPresenter);
                    AdjustCards();
                })
                .AddTo(this);

            deck.Discards.ObserveAdd()
                .Subscribe(e =>
                {
                    var target = cards.First(x => x.Card == e.Value);
                    cards.Remove(target);
                    Destroy(target.gameObject);
                    AdjustCards();
                })
                .AddTo(this);
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
