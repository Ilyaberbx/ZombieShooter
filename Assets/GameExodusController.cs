using UnityEngine;
using Zenject;

namespace FPS
{
    public class GameExodusController 
    {
        private MenuProvider _menuProvider;
        private GameStateController _gameStateController;
        private LevelsProvider _levelsProvider;

        private IGameLogic _gameLogic;

        public GameExodusController(GameStateController gameStateController, MenuProvider menuProvider, LevelsProvider levelsProvider,Player player)
        {
            _gameStateController = gameStateController;
            _menuProvider = menuProvider;
            _levelsProvider = levelsProvider;
            player.OnDied += GameOver;
        }

        public void Win()
        {
            _gameLogic = new GameWinLogic(_gameStateController, _menuProvider, _levelsProvider);
            _gameLogic.Execute();
        }
        public void GameOver()
        {
            _gameLogic = new GameOverLogic(_gameStateController, _menuProvider);
            _gameLogic.Execute();
        }
    }
}
