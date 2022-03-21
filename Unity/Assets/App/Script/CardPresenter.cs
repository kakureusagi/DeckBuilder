using TMPro;
using UnityEngine;

namespace App
{
    public class CardPresenter : MonoBehaviour
    {

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

        public static float Width => 2;
        public static float Height => 2;

        public Collider2D Collider => collider;

        void Awake()
        {
            UnSelect();
        }

        public void Initialize(Card card)
        {
            this.card = card;
            cardName.text = card.Name;
            cost.text = card.Cost.ToString();
            attack.text = card.Attack.ToString();
            block.text = card.Block.ToString();
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
