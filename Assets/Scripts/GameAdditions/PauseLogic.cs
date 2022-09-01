using UnityEngine;

namespace FPS
{
    public class PauseLogic
    {
        private GameStateController _gameStateController;
        private MenuProvider _menuProvider;
        private WeaponInput _weaponInput;
        private DefaultMovementInput _movementInput;

        public PauseLogic(GameStateController gameStateContoller, MenuProvider menuProvider,WeaponInput weaponInput,DefaultMovementInput movementInput)
        {
            _gameStateController = gameStateContoller;
            _menuProvider = menuProvider;
            _weaponInput = weaponInput;
            _movementInput = movementInput;
        }
        public void PauseToogle()
        {
            if (_gameStateController.CurrentState == GameState.GamePlay)
            {
                _gameStateController.SetState(GameState.Pause);
                _menuProvider.PauseSettings();
                _movementInput.Disable();
                _weaponInput.Disable();
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
                Resume();

        }
        public void Resume()
        {
            _gameStateController.SetState(GameState.GamePlay);
            _menuProvider.GamePlay();
            _movementInput.Enable();
            _weaponInput.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
