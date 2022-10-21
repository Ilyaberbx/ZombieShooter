using UnityEngine;

namespace FPS
{
    public class WeaponLauncher : MonoBehaviour 
    {
        private IWeapon _weapon;
        private Gravity _gravity;

        public void Initialize(Player player)
        {
            _gravity = player.GetComponent<Gravity>();
        }

        public void PerformAttack()
        {
            if (!CanAttack()) return;

            _weapon.Attack();
        }
        public void SetWeapon(IWeapon weapon) => this._weapon = weapon;
        private bool CanAttack()
        {
            return _gravity.IsGrounded ? true : false;
        }
    }
}
