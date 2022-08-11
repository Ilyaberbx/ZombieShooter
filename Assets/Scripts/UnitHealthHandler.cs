using UnityEngine;

namespace FPS
{   
    public class UnitHealthHandler
    {
        private int _health;
        private IUnit _unit;

        public void Inititalize(IUnit unit)
        {
            _unit = unit;
            _health = unit.Health;
        }
        public void CalculateHealth(int damage)
        {
            _health -= damage;
            
            if (Mathf.Clamp(_health, 0, _health) == 0)
                _unit.Die();
        }
   
    }
}
