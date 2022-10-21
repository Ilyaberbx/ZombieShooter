using UnityEngine;

namespace FPS
{
    public class GameOverLogic : IGameLogic
    {
        private MenuProvider _menuProvider;
        private GameStateController _gameStateController;
        private WeaponInput _weaponInput;
        private DefaultMovementInput _movementInput;


        public GameOverLogic(GameStateController gameStateController, MenuProvider menuProvider, WeaponInput weaponInput,DefaultMovementInput movementInput)
        {
            _menuProvider = menuProvider;
            _gameStateController = gameStateController;
            _weaponInput = weaponInput;
            _movementInput = movementInput;
        }

        public void Execute()
        {
            _gameStateController.SetState(GameState.Pause);
            _menuProvider.GameOver();
            _weaponInput.Disable();
            _movementInput.Disable();
            Cursor.lockState = CursorLockMode.Confined;     
        }
    }
}
