using UnityEngine;
using Zenject;

namespace FPS
{
    public class WeaponAnimator : GamePlayObjectMono
    {
        private const string IS_SPRINTING = "IsSprinting";

        private Animator _animator;
        private PlayerMovement _playerMovement;

        private void Awake() => Initialize();
        private void Initialize()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponentInParent<PlayerMovement>();
            GameStateController.OnGameStateChanged += OnGameStateChanged;
        }
        private void OnEnable() => _playerMovement.OnSprintingToggled += ToggleSprinting;
        private void OnDisable() => _playerMovement.OnSprintingToggled -= ToggleSprinting;
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;

        private void Update() => CalculateAnimatorSpeed();
        private void CalculateAnimatorSpeed()
        {
            if (_playerMovement.Movement.magnitude == 0)
            {
                _animator.speed = 0;
                return;
            }
            _animator.speed = 1f;
        }
        private void ToggleSprinting(bool isSprinting) => _animator.SetBool(IS_SPRINTING, isSprinting);
    }
}