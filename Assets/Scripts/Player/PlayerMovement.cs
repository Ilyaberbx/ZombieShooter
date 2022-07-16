using System;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class PlayerMovement : GamePlayBehaviour
    {
        [Inject] private DefaultMovementInput _movementInput;

        public event Action<bool> OnSprintingToggled;
        public event Action OnJumped;
        public Vector3 Movement { get; private set; }
        public bool IsSprinting => _isSprinting;

        public float SpeedCoefficient
        {
            get => _speedCoefficient;
            set
            {
                if (value > 0)
                    _speedCoefficient = value;
                else
                    Debug.LogError("Value must be greater than 0");
            }

        }
        private CharacterController _characterController;
        private PlayerStanceChanger _playerStanceSystem;
        private Gravity _gravity;
        private MovementSettings _movementSettings;
        private Vector2 _inputAxes;
        private bool _isSprinting;
        private float _speedCoefficient;

        private void Awake() => Initialize();
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        private void OnEnable()
        {
            _movementInput.Player.Movement.performed += e => _inputAxes = e.ReadValue<Vector2>();
            _movementInput.Player.Jump.performed += e => Jump();
            _movementInput.Player.Sprint.performed += e => ToggleSprinting();
            _movementInput.Enable();
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
            _playerStanceSystem = GetComponent<PlayerStanceChanger>();
            _movementSettings = GetComponent<MovementSettings>();
            _gravity = GetComponent<Gravity>();
            GameStateController.OnGameStateChanged += OnGameStateChanged;
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
            if (_isSprinting)
                if (_inputAxes.y <= _movementSettings.MinVelocityToSprint || !_gravity.TryCatchGround())
                {
                    StopSprinting();
                    SpeedCoefficient = 1f;
                }
        }
        private void Jump()
        {
            if (!_gravity.IsGrounded) return;

            if (_playerStanceSystem.CurrentStance != PlayerStance.Standing)
            {
                if (_playerStanceSystem.CurrentStance == PlayerStance.Crouching)
                {
                    _playerStanceSystem.StandUp();
                    return;
                }

                _playerStanceSystem.Crouch();
                return;

            }

            OnJumped?.Invoke();

            _gravity.Velocity = new Vector3(0, Mathf.Sqrt(_movementSettings.JumpingForce * _gravity.GeneralGravity * _gravity.GroundedGravity), 0);
        }
        private void ToggleSprinting()
        {
            if (_playerStanceSystem.CurrentStance != PlayerStance.Standing)
            {
                StopSprinting();
                return;
            }
            _isSprinting = !_isSprinting;

            OnSprintingToggled?.Invoke(_isSprinting);

            SpeedCoefficient = _isSprinting ? _movementSettings.SprintingSpeedCoefficient : 1f;
            return;

        }

        public void StopSprinting()
        {
            _isSprinting = false;
            OnSprintingToggled?.Invoke(_isSprinting);
        }

    }
}
