using UnityEngine;

namespace FPS
{
    public class EnemyAnimator : MonoBehaviour
    {
        private readonly string IS_CHASING = "IsChasing";
        private readonly string ATTACK = "Attack";
        private readonly string DIE = "Die";
        private Animator _animator;

        private void Awake() => _animator = GetComponent<Animator>();

        public void AnimateChasing(bool isChasing) => _animator.SetBool(IS_CHASING, isChasing);

        public void AnimateAttacking() => _animator.SetTrigger(ATTACK);

        public void AnimateDying() => _animator.SetTrigger(DIE);
    }
}
