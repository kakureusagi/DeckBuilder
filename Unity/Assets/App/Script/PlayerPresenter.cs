using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;

namespace App
{
    public class PlayerPresenter : MonoBehaviour
    {
        public Collider2D Collider => collider;
        
        
        [SerializeField]
        TMP_Text hp = default;

        [SerializeField]
        Collider2D collider = default;

        [SerializeField]
        Animator animator = default;

        Player player;
        UniTaskCompletionSource animationTask;

        public void Initialize(Player player)
        {
            this.player = player;

            player.Hp.Merge(player.MaxHp)
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
            hp.text = $"{player.Hp.Value}/{player.MaxHp.Value}";
        }
    }
}
