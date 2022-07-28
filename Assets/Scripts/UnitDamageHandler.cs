using System;
using UnityEngine;

namespace FPS
{
    public class UnitDamageHandler : MonoBehaviour, IWeaponVisitor
    {
        public event Action<int> OnDamageApplied;
        private IUnit _unit;

        public void Initialize(IUnit unit)
        {
            _unit = unit;
            _unit.OnDied += StopHandleDamage;
        }
        private void OnDestroy() => _unit.OnDied -= StopHandleDamage;
        private void StopHandleDamage() => enabled = false;
        private void ApplyDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            OnDamageApplied?.Invoke(damage);
        }

        public void Visit(Pistol weapon)
        {
            ApplyDamage(weapon.Damage);
            // Should be differentation; 
        }

        public void Visit(Rifle weapon)
        {
            ApplyDamage(weapon.Damage);
            // Should be differentation; 
        }
        public void Visit(DefaultZombieMeleeAttackment defaultZombie)
        {
            ApplyDamage(defaultZombie.Damage);

            Debug.Log("PlayerAttacked");
        }
    }
}