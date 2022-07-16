using System;

namespace FPS
{
    public interface IWeapon
    {
        void Attack();
        void StartAttacking();
        void StopAttacking();

        event Action<bool> OnAttacked;
        
    }
}
