using System;

namespace FPS
{
    public interface IWeapon
    {
        WeaponType GetWeaponType();
        int Damage { get; }
        void Attack();
        
    }
}
