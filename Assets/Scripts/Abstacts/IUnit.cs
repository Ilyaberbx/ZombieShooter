using System;

namespace FPS

{
    public interface IUnit 
    {
        UnitDamageHandler UnitDamageHandler { get; }
        UnitHealthHandler UnitHealthHandler { get; }

        int Health { get; }
        void Die();

        event Action OnDied;
    }
}
