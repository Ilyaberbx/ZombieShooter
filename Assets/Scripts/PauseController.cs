using UnityEngine;
using Zenject;

namespace FPS
{
    public class PauseController : InGameBehaviour
    {
        [Inject] private DefaultMovementInput _input;
        [Inject] private WeaponInput _weaponInput;
        [Inject] private MenuProvider _menuProvider;
        private PauseLogic _pauseLogic;
        private void Awake()
        {
            Initialize();
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            _input.Player.Pause.performed += e => _pauseLogic.PauseToogle();
        }
        private void OnDestroy()
        {
            GameStateController.OnGameStateChanged -= OnGameStateChanged;
            _input.Player.Pause.performed -= e => _pauseLogic.PauseToogle();
            _input.Disable();
        }
        private void Initialize() => _pauseLogic = new PauseLogic(GameStateController, _menuProvider, _weaponInput, _input);
        public void Resume()
        {
            if (_pauseLogic != null)
                _pauseLogic.Resume();
        }
    }
}
