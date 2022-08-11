using UnityEngine;
using Zenject;

namespace FPS
{
    public class PauseController : InGameBehaviour
    {
        [Inject] private DefaultMovementInput _input;

        private PauseLogic _pauseLogic;
        private void Awake()
        {
            Initialize();

            if (_pauseLogic != null)
            _input.Player.Pause.performed += e => _pauseLogic.PauseToogle();

            GameStateController.OnGameStateChanged += OnGameStateChanged;
        }
        private void OnDestroy()
        {
            _input.Player.Pause.performed -= e => _pauseLogic.PauseToogle();
            GameStateController.OnGameStateChanged -= OnGameStateChanged;
        }

        private void Initialize() => _pauseLogic = new PauseLogic(GameStateController);
        public void Resume()
        {
            if (_pauseLogic != null)
                _pauseLogic.Resume();
        }
        protected override void OnGameStateChanged(GameState newState)
        {
            if (GameStateController.CurrentState == GameState.GameOver)
            {
                _input.Player.Pause.performed -= e => _pauseLogic.PauseToogle();
                _pauseLogic = null;
            }

        }
    }
}
