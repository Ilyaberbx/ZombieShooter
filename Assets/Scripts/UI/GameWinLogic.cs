
using UnityEngine;
using Zenject;

namespace FPS
{
    public class GameWinLogic : IGameLogic
    {
        private MenuProvider _menuProvider;
        private GameStateController _gameStateController;
        private LevelsProvider _levelsProvider;
        private WeaponInput _weaponInput;
        private DefaultMovementInput _movementInput;
        private Wallet _wallet;

        public GameWinLogic(GameStateController gameStateController, MenuProvider menuProvider, LevelsProvider levelsProvider,WeaponInput weaponInput, DefaultMovementInput movementInput,Wallet wallet)
        {
            _menuProvider = menuProvider;
            _gameStateController = gameStateController;
            _levelsProvider = levelsProvider;
            _weaponInput = weaponInput;
            _movementInput = movementInput;
            _wallet = wallet;
        }

        public void Execute()
        {
            _wallet.AddCoins(_levelsProvider.CurrentLevel.LevelReward);
            _gameStateController.SetState(GameState.Pause);
            _menuProvider.GameWin();
            _weaponInput.Disable();
            _movementInput.Disable();
            _levelsProvider.CompleteLevel();
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
