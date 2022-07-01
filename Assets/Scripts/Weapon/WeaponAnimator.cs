using UnityEngine;
using Zenject;

namespace FPS
{
    public class WeaponAnimator : GamePlayObjectMono
    {
        private const string IS_SPRINTING = "IsSprinting";
        private const string IS_GROUNDED = "IsGrounded";

        private Animator _animator;
        private PlayerMovement _playerMovement;
        private Gravity _gravity;

        private void Awake() => Initialize();

        private void OnEnable() => _playerMovement.OnSprintingToggled += ToggleSprinting;

        private void OnDisable() => _playerMovement.OnSprintingToggled -= ToggleSprinting;

        private void Update()
        {
            CalculateAnimatorSpeed();
            CalculateAnimatorFalling();
        }

        private void Initialize()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponentInParent<PlayerMovement>();
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
                while(_animator.hasBoundPlayables) { }

                _animator.speed = 0;
                return;
            }

            _animator.speed = 1f;
        }
        private void CalculateAnimatorFalling() => _animator.SetBool(IS_GROUNDED, _gravity.IsGrounded);
        private void ToggleSprinting(bool isSprinting) => _animator.SetBool(IS_SPRINTING, isSprinting);
    }
}