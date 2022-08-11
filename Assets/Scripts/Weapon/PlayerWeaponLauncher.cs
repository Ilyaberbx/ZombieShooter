namespace FPS
{
    public class PlayerWeaponLauncher : InGameBehaviour
    {
        private IWeapon _weapon;
        private PlayerMovement _playerMovement;
        private Gravity _gravity;

        public void Initialize(Player player)
        {
            _playerMovement = player.GetComponent<PlayerMovement>();
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
