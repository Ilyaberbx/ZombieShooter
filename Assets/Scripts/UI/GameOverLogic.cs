using UnityEngine;

namespace FPS
{
    public class GameOverLogic : IGameLogic
    {
        private MenuProvider _menuProvider;
        private GameStateController _gameStateController;


        public GameOverLogic(GameStateController gameStateController, MenuProvider menuProvider)
        {
            _menuProvider = menuProvider;
            _gameStateController = gameStateController;
        }

        public void Execute()
        {
            _gameStateController.SetState(GameState.Pause);
            _menuProvider.GameOver();
            Cursor.lockState = CursorLockMode.Confined;     
        }
    }
}
