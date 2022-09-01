using UnityEngine;
using Zenject;

namespace FPS
{
    public class LocationStart : MonoBehaviour
    {
        [Inject] private GameStateController _gameStateController;
        [Inject] private WeaponInput _weaponInput;
        [Inject] private DefaultMovementInput _defaultInput;

        private void Awake()
        {         
            _gameStateController.SetState(GameState.GamePlay);
            _defaultInput.Enable();
            _weaponInput.Enable();
        }
        private void OnDisable()
        {
            _defaultInput.Disable();
            _weaponInput.Disable();
        }
    } 
}
