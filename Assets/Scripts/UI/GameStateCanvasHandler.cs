using UnityEngine;

namespace FPS
{
    public class GameStateCanvasHandler : InGameBehaviour
    {
        [SerializeField] private GameOverCanvas _gameOverCanvas;
        [SerializeField] private PauseCanvas _pauseCanvas;
        [SerializeField] private GamePlayCanvas _gamePlayCanvas;

        private void Awake() => GameStateController.OnGameStateChanged += this.OnGameStateChanged;
        private void OnDestroy() => GameStateController.OnGameStateChanged -= this.OnGameStateChanged;

        protected override void OnGameStateChanged(GameState newState)
        {
            _pauseCanvas.gameObject.SetActive(GameStateController.CurrentState == GameState.Pause);
            _gameOverCanvas.gameObject.SetActive(GameStateController.CurrentState == GameState.GameOver);
            _gamePlayCanvas.gameObject.SetActive(GameStateController.CurrentState == GameState.GamePlay);
        }
    }
}

