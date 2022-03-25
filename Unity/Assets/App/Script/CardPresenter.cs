using TMPro;
using UnityEngine;
#pragma warning disable CS0108, CS0114

namespace App
{
    public class CardPresenter : MonoBehaviour
    {

        public Card Card => card;
        public Collider2D Collider => collider;
        public static float Width => 2;
        public static float Height => 2;

        [SerializeField]
        GameObject isSelected = default;

        [SerializeField]
        TMP_Text cardName = default;

        [SerializeField]
        TMP_Text cost = default;

        [SerializeField]
        TMP_Text attack = default;

        [SerializeField]
        TMP_Text block = default;

        [SerializeField]
        Collider2D collider = default;

        Card card;

        void Awake()
        {
            UnSelect();
        }

        public void Initialize(Card card)
        {
            this.card = card;
            cardName.text = card.Name;
            cost.text = $"コスト:{card.Cost}";
            attack.text = $"アタック:{card.Attack}";
            block.text = $"ブロック:{card.Block}";
        }

        public void Select()
        {
            isSelected.SetActive(true);
        }

        public void UnSelect()
        {
            isSelected.SetActive(false);
        }
    }
}
