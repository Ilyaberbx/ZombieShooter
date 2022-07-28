using System;

namespace FPS
{
    public interface IWeapon
    {
        int Damage { get; }
        void Attack();
        
    }
}
