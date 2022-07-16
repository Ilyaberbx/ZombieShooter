using System.Collections;
using UnityEngine;

namespace FPS
{
    public class CameraStanceHandler : GamePlayBehaviour
    {
        [Header("Stance Camera Settings")]
        [SerializeField] private float _stanceSmoothing;
        public float CurrentDesireHeight
        {
            get { return _currentDesireHeight; }
            set
            {
                if (_calculatingViewRoutine != null)
                    StopCoroutine(_calculatingViewRoutine);

                _currentDesireHeight = value;
                _calculatingViewRoutine = StartCoroutine(CalculateViewRoutine());

            }
        }

        private float _cameraHeightVelocity;
        private float _currentDesireHeight;
        private Coroutine _calculatingViewRoutine;

        private void Awake() => Initialize();
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        private void Initialize() => GameStateController.OnGameStateChanged += OnGameStateChanged;

        private IEnumerator CalculateViewRoutine()
        {
            while (transform.localPosition.y != _currentDesireHeight)
            {
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    Mathf.SmoothDamp(transform.localPosition.y, _currentDesireHeight, ref _cameraHeightVelocity, _stanceSmoothing),
                    transform.localPosition.z);
                yield return null;
            }
        }

    }
}