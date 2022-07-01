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
        public float SpeedCoefficient { get; private set; }

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
            SpeedCoefficient = 1f;
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
            _characterController = GetComponent<CharacterController>();
            _playerStanceSystem = GetComponent<PlayerStanceSystem>();
            _movementSettings = GetComponent<MovementSettings>();
            _gravity = GetComponent<Gravity>();
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            _playerStanceSystem.OnStanceChanged += OnStanceChanged;
        }
        private void CalculateMovement()
        {
            var verticalSpeed = _movementSettings.WalkingForwardSpeed * _inputAxes.y * SpeedCoefficient * Time.deltaTime;
            var horizontalSpeed = _movementSettings.WalkingStrafeSpeed * _inputAxes.x * SpeedCoefficient * Time.deltaTime;
            Movement = transform.right * horizontalSpeed + transform.forward * verticalSpeed;

            CalculateSprintingStop();

            if (Movement.magnitude <= 0f) return;

            _characterController.Move(Movement);             
        }
        private void CalculateSprintingStop()
        {
            if (_inputAxes.y <= _movementSettings.MinVelocityToSprint || !_gravity.TryCatchGround())
                StopSprint();
        }
        private void Jump()
        {
            if (!_gravity.IsGrounded) return;

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
                SpeedCoefficient = _movementSettings.SprintingSpeedUpCoefficient;
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
                case PlayerStance.Standing:
                    SpeedCoefficient = 1f;
                    break;
                case PlayerStance.Crouching:
                    SpeedCoefficient = _movementSettings.CrouchingSpeedUpCoefficient;
                    break;
                case PlayerStance.Prone:
                    SpeedCoefficient = _movementSettings.ProneSpeedUpCoefficient;
                    break;
            }
        }
        private void StopSprint()
        {
            _isSprinting = false;
            SpeedCoefficient = 1f;
            OnSprintingToggled?.Invoke(_isSprinting);
        }

    }
}
