
using UnityEngine;
using Zenject;

namespace FPS
{
    public class GameWinLogic : IGameLogic
    {
        private MenuProvider _menuProvider;
        private GameStateController _gameStateController;
        private LevelsProvider _levelsProvider;
        public GameWinLogic(GameStateController gameStateController, MenuProvider menuProvider, LevelsProvider levelsProvider)
        {
            _menuProvider = menuProvider;
            _gameStateController = gameStateController;
            _levelsProvider = levelsProvider;
        }

        public void Execute()
        {
            _gameStateController.SetState(GameState.Pause);
            _menuProvider.GameWin();
            _levelsProvider.CompleteLevel();
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
