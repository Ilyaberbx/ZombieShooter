using System;

namespace FPS

{
    public interface IUnit 
    {
        UnitDamageHandler UnitDamageHandler { get; }
        UnitHealth UnitHealth { get; }

        void Die();

        event Action OnDied;
    }
}
