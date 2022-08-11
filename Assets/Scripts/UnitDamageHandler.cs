using System;

namespace FPS
{
    public class UnitDamageHandler
    {
        public event Action<int> OnDamageApplied;
        private IUnit _unit;

        public void Initialize(IUnit unit)
        {
            _unit = unit;
        }
        public void ApplyDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            _unit.UnitHealthHandler.CalculateHealth(damage);
            OnDamageApplied?.Invoke(damage);
        }

    }
}