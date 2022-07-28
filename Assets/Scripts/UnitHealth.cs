using UnityEngine;

namespace FPS
{
    [RequireComponent(typeof(UnitDamageHandler))]
    public class UnitHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;

        private int _currentHealth;
        private IUnit _unit;

        public void Inititalize(IUnit unit)
        {
            _unit = unit;
            _unit.UnitDamageHandler.OnDamageApplied += CalculateHealth;
            _currentHealth = _maxHealth;
        }
        private void OnDestroy() => _unit.UnitDamageHandler.OnDamageApplied -= CalculateHealth;
        public void CalculateHealth(int damage)
        {
            _currentHealth -= damage;
            
            if (Mathf.Clamp(_currentHealth, 0, _maxHealth) == 0)
                _unit.Die();
        }
   
    }
}
