using UnityEngine;

namespace FPS
{
    public class WeaponAnimator : GamePlayBehaviour
    {
        private const string IS_SPRINTING = "IsSprinting";
        private const string IS_GROUNDED = "IsGrounded";
        private const string IS_MOVING = "IsMoving";
        private const string IS_ATTACKING = "IsAttacking";
        protected const string IS_RELOADING = "IsReloading";

        protected Animator _animator;
        private PlayerMovement _playerMovement;
        private IWeapon _weapon;
        private Gravity _gravity;

        private void Awake() => Initialize();

        private void OnEnable()
        {
            _playerMovement.OnSprintingToggled += ToggleSprinting;
            _weapon.OnAttacked += CalculateAttackment;
             ToggleSprinting(_playerMovement.IsSprinting);
        }

        private void OnDisable()
        {
            _playerMovement.OnSprintingToggled -= ToggleSprinting;
            _weapon.OnAttacked -= CalculateAttackment;
        }

        private void Update()
        {
            CalculateAnimatorSpeed();
            CalculateAnimatorFalling();
        }

        private void Initialize()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
            _animator = GetComponent<Animator>();
            _weapon = GetComponentInParent<IWeapon>();
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
        private void ToggleSprinting(bool isSprinting) => _animator.SetBool(IS_SPRINTING, isSprinting);
        private void CalculateAttackment(bool isAttacking) => _animator.SetBool(IS_ATTACKING, isAttacking);

        
    }
}