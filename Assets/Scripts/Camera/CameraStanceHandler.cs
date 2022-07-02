using UnityEngine;

namespace FPS
{
    public class CameraStanceHandler : GamePlayObjectMono
    {
        [Header("Stance Camera Settings")]
        [SerializeField] private float _stanceSmoothing; 
        public float CurrentDesireHeight { get; set; }

        private float _cameraHeightVelocity;

        private void Awake() => Initialize();
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        private void Update() => CalculateView();
        private void Initialize() => GameStateController.OnGameStateChanged += OnGameStateChanged;

        private void CalculateView()
        {
            if (transform.localPosition.y == CurrentDesireHeight) return;

            transform.localPosition = new Vector3(
                transform.localPosition.x,
                Mathf.SmoothDamp(transform.localPosition.y, CurrentDesireHeight, ref _cameraHeightVelocity, _stanceSmoothing),
                transform.localPosition.z);
        }

    }
}