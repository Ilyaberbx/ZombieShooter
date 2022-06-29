using UnityEngine;

namespace FPS
{
    public class CameraStanceHandler : GamePlayObjectMono
    {
        [Header("Stance Camera Settings")]
        [SerializeField] private float _cameraStandingHeight;
        [SerializeField] private float _cameraCrouchingHeight;
        [SerializeField] private float _cameraProneHeight;
        [SerializeField] private float _stanceSmoothing;

        private PlayerStance _currentStance;
        private float _currentDesireHeight;
        private float _cameraHeightVelocity;

        private PlayerStanceSystem _stanceSystem;
        private void Awake() => Initialize();
        private void OnDestroy()
        {
            _stanceSystem.OnStanceChanged -= OnStanceChanged;
            GameStateController.OnGameStateChanged -= OnGameStateChanged;
        }
        private void Update() => CalculateView();
        private void Initialize()
        {
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            _stanceSystem = GetComponentInParent<PlayerStanceSystem>();
            _stanceSystem.OnStanceChanged += OnStanceChanged;
        }
        private void OnStanceChanged(PlayerStance newStance)
        {
            _currentStance = newStance;
            CalculateCameraDirection();
        }
        private void CalculateCameraDirection()
        {
            switch (_currentStance)
            {
                case PlayerStance.Standing:
                    _currentDesireHeight = _cameraStandingHeight;
                    break;
                case PlayerStance.Crouching:
                    _currentDesireHeight = _cameraCrouchingHeight;
                    break;
                case PlayerStance.Prone:
                    _currentDesireHeight = _cameraProneHeight;
                    break;
            }
        }
        private void CalculateView()
        {
            if (transform.localPosition.y == _currentDesireHeight) return;

            transform.localPosition = new Vector3(
                transform.localPosition.x,
                Mathf.SmoothDamp(transform.localPosition.y, _currentDesireHeight, ref _cameraHeightVelocity, _stanceSmoothing),
                transform.localPosition.z);
        }

    }
}