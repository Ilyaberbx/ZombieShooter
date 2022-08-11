using UnityEngine;
using Zenject;

namespace FPS
{
    public class CameraFollowing : InGameBehaviour
    {
        [Inject] private DefaultMovementInput _movementInput;

        [SerializeField] private Settings _settings;

        private PlayerMovement _player;
        private Vector2 _inputView;
        private int _viewClampMin = -70;
        private int _viewClampMax = 80;
        private float _xRotation;

        private void Awake() => Initialize();
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;

        private void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            _player = GetComponentInParent<PlayerMovement>();
        }   
        private void OnEnable() => _movementInput.Player.View.performed += e => _inputView = e.ReadValue<Vector2>();
        private void OnDisable() => _movementInput.Player.View.performed -= e => _inputView = e.ReadValue<Vector2>();

        private void Update() => CalculateView();
        private void CalculateView()
        {
            var mouseX = _inputView.x * _settings.ViewXSensetivity * _settings.ViewInverted * Time.deltaTime;
            var mouseY = _inputView.y * _settings.ViewYSensetivity * _settings.ViewInverted * Time.deltaTime;
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, _viewClampMin, _viewClampMax);
            transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _player.transform.Rotate(Vector3.up * mouseX);
        }
    }
}
