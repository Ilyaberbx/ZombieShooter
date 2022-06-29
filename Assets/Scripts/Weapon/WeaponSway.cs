using UnityEngine;
using Zenject;

namespace FPS
{
    public class WeaponSway : GamePlayObjectMono
    {
        [Inject] private DefaultMovementInput _movementInput;

        [SerializeField] private Settings _settings;

        [Header("Settings")]
        [SerializeField] private float _swayAmount;
        [SerializeField] private float _swaySmoothing;
        [SerializeField] private float _swayResetSmoothing;
        [SerializeField] private float _swayClampX;
        [SerializeField] private float _swayClampY;

        private Vector3 _newWeaponRotation;
        private Vector3 _targetWeaponRotation;
        private Vector3 _targetWeaponRotationVelocity;
        private Vector3 _newWeaponRotationVelocity;
        private Vector2 _inputView;

        private void Awake() => Inititalize();
        private void Update() => CalculateSwaying();
        private void OnEnable() => _movementInput.Player.View.performed += e => _inputView = e.ReadValue<Vector2>();
        private void OnDisable() => _movementInput.Player.View.performed -= e => _inputView = e.ReadValue<Vector2>();
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        private void Inititalize() => GameStateController.OnGameStateChanged += OnGameStateChanged;
        private void CalculateSwaying()
        {
            var mouseX = _inputView.x * _swayAmount * _settings.ViewInverted * Time.deltaTime;
            var mouseY = _inputView.y * _swayAmount * _settings.ViewInverted * Time.deltaTime;

            _targetWeaponRotation = new Vector3(-mouseY, mouseX);

            CalculateClamping();
            CalculateSmoothing();

            transform.localRotation = Quaternion.Euler(_newWeaponRotation);
        }
        private void CalculateSmoothing()
        {
            _targetWeaponRotation = Vector3.SmoothDamp(_targetWeaponRotation, transform.forward, ref _targetWeaponRotationVelocity, _swayResetSmoothing);
            _newWeaponRotation = Vector3.SmoothDamp(_newWeaponRotation, _targetWeaponRotation, ref _newWeaponRotationVelocity, _swaySmoothing);
        }
        private void CalculateClamping()
        {
            _targetWeaponRotation.x = Mathf.Clamp(_targetWeaponRotation.x, -_swayClampX, _swayClampX);
            _targetWeaponRotation.y = Mathf.Clamp(_targetWeaponRotation.y, -_swayClampY, _swayClampY);
        }
    }

}
