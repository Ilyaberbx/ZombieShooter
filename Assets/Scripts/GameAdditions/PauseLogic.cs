using UnityEngine;

namespace FPS
{
    public class PauseLogic
    {
        private GameStateController _gameStateController;      
        public PauseLogic(GameStateController gameStateContoller)
        {
            _gameStateController = gameStateContoller;
        }
        public void PauseToogle()
        {
            if (_gameStateController.CurrentState == GameState.GamePlay)
            {
                _gameStateController.SetState(GameState.Pause);
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0;
            }
            else
                Resume();

        }
        public void Resume()
        {
            _gameStateController.SetState(GameState.GamePlay);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
