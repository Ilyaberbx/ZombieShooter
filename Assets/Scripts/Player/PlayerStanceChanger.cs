using System;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class PlayerStanceChanger : GamePlayBehaviour
    {
        [Inject] private DefaultMovementInput _movementInput;
        public PlayerStance CurrentStance { get; private set; }

        [SerializeField] private Stance _playerStanding;
        [SerializeField] private Stance _playerCrouching;
        [SerializeField] private Stance _playerProne;

        [SerializeField] private CameraStanceHandler _cameraStance;
        [SerializeField] private Transform _feetPosition;

        private PlayerMovement _playerMovement;
        private MovementSettings _movementSettings;
        private CharacterController _characterController;

        private void Awake() => Inititalize();
        private void Inititalize()
        {
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            _playerMovement = GetComponent<PlayerMovement>();
            _movementSettings = GetComponent<MovementSettings>();
            _characterController = GetComponent<CharacterController>();
            StandUp();
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
        private void SetStance(PlayerStance newStance)
        {
            if (CurrentStance == newStance) return;

            CurrentStance = newStance;
        }
        public void Crouch()
        {
            if (CurrentStance == PlayerStance.Crouching)
            {
                StandUp();
                return;
            }

            if (!CanChangeStance(_playerCrouching.StanceCollider.height)) return;

            SetStance(PlayerStance.Crouching);

            SetCollider(_playerCrouching);

            _cameraStance.CurrentDesireHeight = _playerCrouching.CameraHeight;
            _playerMovement.SpeedCoefficient = _movementSettings.CrouchingSpeedCoefficient;
            _playerMovement.StopSprinting();
        }
        public void Prone()
        {
            if (!CanChangeStance(_playerProne.StanceCollider.height)) return;

            SetStance(PlayerStance.Prone);

            SetCollider(_playerProne);

            _cameraStance.CurrentDesireHeight = _playerProne.CameraHeight;
            _playerMovement.SpeedCoefficient = _movementSettings.ProneSpeedUpCoefficient;
            _playerMovement.StopSprinting();
        }
        public void StandUp()
        {
            if (!CanChangeStance(_playerStanding.StanceCollider.height)) return;

            SetStance(PlayerStance.Standing);

            SetCollider(_playerStanding);

            _cameraStance.CurrentDesireHeight = _playerStanding.CameraHeight;
            _playerMovement.SpeedCoefficient = 1f;
        }
        private void SetCollider(Stance stance)
        {
            _characterController.height = stance.StanceCollider.height;
            _characterController.radius = stance.StanceCollider.radius;
            _characterController.center = stance.StanceCollider.center;
        }
        private bool CanChangeStance(float colliderHeight)
        {
            Vector3 start = new Vector3(_feetPosition.position.x, _feetPosition.position.y + colliderHeight + _characterController.radius, _feetPosition.position.z);
            Vector3 end = new Vector3(_feetPosition.position.x, _feetPosition.position.y, _feetPosition.position.z);

            var hit = Physics.OverlapCapsule(start, end, _characterController.radius);

            Ground ground;

            foreach (var check in hit)
            {
                if (check.TryGetComponent(out ground))
                    return false;
            }
            return true;
        }
    }

}