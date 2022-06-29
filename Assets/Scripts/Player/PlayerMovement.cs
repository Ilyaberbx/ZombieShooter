using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace FPS
{
    public class PlayerMovement : GamePlayObjectMono
    {
        [Inject] private DefaultMovementInput _movementInput;

        public Action<bool> OnSprintingToggled;

        public Vector3 Movement { get; private set; }

        private CharacterController _characterController;
        private PlayerStanceSystem _playerStanceSystem;
        private Gravity _gravity;
        private MovementSettings _movementSettings;
        private Vector2 _inputAxes;
        private bool _isSprinting;
        public float _speedCoefficient;

        private void Awake() => Initialize();
        private void OnDestroy()
        {
            GameStateController.OnGameStateChanged -= OnGameStateChanged;
            _playerStanceSystem.OnStanceChanged += OnStanceChanged;
        }
        private void OnEnable()
        {
            _movementInput.Player.Movement.performed += e => _inputAxes = e.ReadValue<Vector2>();
            _movementInput.Player.Jump.performed += e => Jump();
            _movementInput.Player.Sprint.performed += e => ToggleSprinting();
            _movementInput.Enable();
            _speedCoefficient = 1f;
        }
        private void OnDisable()
        {
            _movementInput.Player.Movement.performed -= e => _inputAxes = e.ReadValue<Vector2>();
            _movementInput.Player.Jump.performed -= e => Jump();
            _movementInput.Player.Sprint.performed -= e => ToggleSprinting();
        }
        private void Update() => CalculateMovement();
        private void Initialize()
        {
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            _characterController = GetComponent<CharacterController>();
            _playerStanceSystem = GetComponent<PlayerStanceSystem>();
            _movementSettings = GetComponent<MovementSettings>();
            _gravity = GetComponent<Gravity>();
            _playerStanceSystem.OnStanceChanged += OnStanceChanged;
        }
        private void CalculateMovement()
        {
            var verticalSpeed = _movementSettings.WalkingForwardSpeed * _inputAxes.y * _speedCoefficient * Time.deltaTime;
            var horizontalSpeed = _movementSettings.WalkingStrafeSpeed * _inputAxes.x * _speedCoefficient * Time.deltaTime;
            Movement = transform.right * horizontalSpeed + transform.forward * verticalSpeed;
            _characterController.Move(Movement);
            CalculateSprintingStop();
        }
        private void CalculateSprintingStop()
        {
            if (!_isSprinting) return;

            if (_inputAxes.y <= _movementSettings.MinVelocityToSprint || !_gravity.TryCatchGround())
                StopSprint();

        }
        private void Jump()
        {
            if (!_gravity.TryCatchGround()) return;

            if (_playerStanceSystem.CurrentStance != PlayerStance.Standing)
            {
                _playerStanceSystem.SetStance(_playerStanceSystem.CurrentStance + 1);
                return;
            }

            _gravity.Velocity = new Vector3(0, Mathf.Sqrt(_movementSettings.JumpingForce * _gravity.GeneralGravity * _gravity.GroundedGravity), 0);
        }
        private void ToggleSprinting()
        {
            if (_playerStanceSystem.CurrentStance != PlayerStance.Standing) return;

            _isSprinting = !_isSprinting;

            OnSprintingToggled?.Invoke(_isSprinting);

            if (_isSprinting)
            {
                _speedCoefficient = _movementSettings.SprintingSpeedUpCoefficient;
                return;
            }

            StopSprint();
        }
        private void OnStanceChanged(PlayerStance newStance)
        {
            if (newStance != PlayerStance.Standing)
                StopSprint();

            switch (newStance)
            {
                case PlayerStance.Crouching:
                    _speedCoefficient = _movementSettings.CrouchingSpeedUpCoefficient;
                    break;
                case PlayerStance.Prone:
                    _speedCoefficient = _movementSettings.ProneSpeedUpCoefficient;
                    break;
            }
        }
        private void StopSprint()
        {
            _isSprinting = false;
            _speedCoefficient = 1f;
            OnSprintingToggled?.Invoke(_isSprinting);
        }

    }
}
