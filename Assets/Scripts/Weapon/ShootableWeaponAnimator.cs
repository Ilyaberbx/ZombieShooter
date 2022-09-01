using UnityEngine;

namespace FPS
{
    public class ShootableWeaponAnimator : InGameBehaviour
    {
        private const string IS_SPRINTING = "IsSprinting";
        private const string IS_GROUNDED = "IsGrounded";
        private const string IS_MOVING = "IsMoving";
        private const string IS_ATTACKING = "IsAttacking";
        protected const string IS_RELOADING = "IsReloading";

        protected Animator _animator;
        private PlayerMovement _playerMovement;
        private BaseShootableWeapon _weapon;
        private Gravity _gravity;

        private void Awake() => Initialize();

        private void OnEnable() => _weapon.OnAttacking += CalculateAttackment;

        private void OnDisable() => _weapon.OnAttacking -= CalculateAttackment;

        private void Update()
        {
            CalculateAnimatorSpeed();
            CalculateAnimatorFalling();
        }

        private void Initialize()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
            _animator = GetComponent<Animator>();
            _weapon = GetComponentInParent<BaseShootableWeapon>();
            _gravity = GetComponentInParent<Gravity>();
            GameStateController.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;

        private void CalculateAnimatorSpeed()
        {
            var movementMagnitude = _playerMovement.Movement.magnitude;
            var gravityMagnitude = _playerMovement.GetComponent<CharacterController>().velocity.magnitude;

            if (movementMagnitude == 0 && gravityMagnitude == 0)
            {
                _animator.SetBool(IS_MOVING, false);
                return;
            }

            _animator.SetBool(IS_MOVING, true);
        }
        private void CalculateAnimatorFalling() => _animator.SetBool(IS_GROUNDED, _gravity.IsGrounded);
        private void CalculateAttackment(bool isAttacking) => _animator.SetBool(IS_ATTACKING, isAttacking);
        public void CalculateReloading(bool isReloading) => _animator.SetBool(IS_RELOADING, isReloading);

        protected override void OnGameStateChanged(GameState newState) => _animator.speed = newState == GameState.Pause ? 0 : 1;

    }
}