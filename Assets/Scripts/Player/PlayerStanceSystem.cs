using System;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class PlayerStanceSystem : GamePlayObjectMono
    {
        [Inject] private DefaultMovementInput _movementInput;

        [SerializeField] private CapsuleCollider _playerStandingCollider;
        [SerializeField] private CapsuleCollider _playerCrouchingCollider;
        [SerializeField] private CapsuleCollider _playerProneCollider;

        public event Action<PlayerStance> OnStanceChanged;
        public PlayerStance CurrentStance { get; private set; }

        private void Awake() => Inititalize();
        private void Inititalize()
        {
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            SetStance(PlayerStance.Standing);
        }
        private void OnEnable()
        {
            _movementInput.Player.Crouch.performed += e => Crouch();
            _movementInput.Player.Prone.performed += e => Prone();
        }
        private void OnDisable()
        {
            _movementInput.Player.Crouch.performed -= e => Crouch();
            _movementInput.Player.Prone.performed -= e => Prone();
        }
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        public void SetStance(PlayerStance newStance)
        {
            if (CurrentStance == newStance) return;

            CurrentStance = newStance;
            OnStanceChanged(newStance);
        }
        private void Crouch()
        {
            if (CurrentStance == PlayerStance.Crouching)
            {
                SetStance(PlayerStance.Standing);
                return;
            }

            SetStance(PlayerStance.Crouching);
            CalculateColliderPlacement();
        }
        private void Prone()
        {
            SetStance(PlayerStance.Prone);
            CalculateColliderPlacement();
        }
        private void CalculateColliderPlacement()
        {
            switch (CurrentStance)
            {
                case PlayerStance.Standing:
                    _playerStandingCollider.gameObject.SetActive(true);
                    _playerCrouchingCollider.gameObject.SetActive(false);
                    _playerProneCollider.gameObject.SetActive(false);
                    break;
                case PlayerStance.Crouching:
                    _playerStandingCollider.gameObject.SetActive(false);
                    _playerCrouchingCollider.gameObject.SetActive(true);
                    _playerProneCollider.gameObject.SetActive(false);
                    break;
                case PlayerStance.Prone:
                    _playerStandingCollider.gameObject.SetActive(false);
                    _playerCrouchingCollider.gameObject.SetActive(false);
                    _playerProneCollider.gameObject.SetActive(true);
                    break;
            }
        }
    }

}