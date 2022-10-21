using UnityEngine;
using Zenject;

namespace FPS
{
    public class GameExodusController 
    {
        private MenuProvider _menuProvider;
        private GameStateController _gameStateController;
        private LevelsProvider _levelsProvider;
        private WeaponInput _weaponInput;
        private DefaultMovementInput _movementInput;
        private Wallet _wallet;

        private IGameLogic _gameLogic;

        public GameExodusController(GameStateController gameStateController, MenuProvider menuProvider, LevelsProvider levelsProvider,Player player, WeaponInput weaponInput, DefaultMovementInput movementInput,Wallet wallet)
        {
            _gameStateController = gameStateController;
            _menuProvider = menuProvider;
            _levelsProvider = levelsProvider;
            _weaponInput = weaponInput;
            _movementInput = movementInput;
            _wallet = wallet;
            player.OnDied += GameOver;
        }

        public void Win()
        {
            _gameLogic = new GameWinLogic(_gameStateController, _menuProvider, _levelsProvider, _weaponInput, _movementInput, _wallet);
            _gameLogic.Execute();
        }
        public void GameOver()
        {
            _gameLogic = new GameOverLogic(_gameStateController, _menuProvider, _weaponInput, _movementInput);
            _gameLogic.Execute();
        }
    }
}
