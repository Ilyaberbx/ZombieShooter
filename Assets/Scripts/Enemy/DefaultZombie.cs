using System.Threading.Tasks;
using Zenject;

namespace FPS
{
    public class DefaultZombie : BaseEnemy
    {
        public override async void Attack()
        {
            if (TargetChaser.enabled == false) return;

            SetChasing(false);
            Animator.AnimateAttacking();

            Weapon.Attack();

            await Task.Delay(TimeBeforeChase);

            SetChasing(true);

            OnGameStateChanged(GameStateController.CurrentState);
        }
    }
}
