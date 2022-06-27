using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace FPS
{
    public class PlayerMovement : GamePlayObjectMono
    {
        [Inject] private BasicFPSInput _movementInput;

        private CharacterController _characterController;
        private Gravity _gravity;
        private MovementSettings _movementSettings;
        private Vector2 _inputMovement;

        private void Awake() => Initialize();
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        private void OnEnable()
        {
            _movementInput.Player.Movement.performed += e => _inputMovement = e.ReadValue<Vector2>();
            _movementInput.Player.Jump.performed += Jump;
            _movementInput.Enable();
        }
        private void OnDisable()
        {
            _movementInput.Player.Movement.performed -= e => _inputMovement = e.ReadValue<Vector2>();
            _movementInput.Player.Jump.performed -= Jump;
        }
        private void Update() => CalculateMovement();
        private void Initialize()
        {
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            _characterController = GetComponent<CharacterController>();
            _movementSettings = GetComponent<MovementSettings>();
            _gravity = GetComponent<Gravity>();
        }
        private void CalculateMovement()
        {
            var verticalSpeed = _movementSettings.WalkingForwardSpeed * _inputMovement.y * Time.deltaTime;
            var horizontalSpeed = _movementSettings.WalkingStrafeSpeed * _inputMovement.x * Time.deltaTime;
            var movement = transform.right * horizontalSpeed + transform.forward * verticalSpeed;
            _characterController.Move(movement);
        }
        private void Jump(InputAction.CallbackContext context)
        {
            if (!_gravity.TryCatchGround()) return;

            _gravity.Velocity = new Vector3(0, Mathf.Sqrt(_movementSettings.JumpingForce * _gravity.GeneralGravity * _gravity.GroundedGravity), 0);
        }
    }
}
