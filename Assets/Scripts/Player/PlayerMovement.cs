using UnityEngine;
using Zenject;

namespace FPS
{
    public class PlayerMovement : MonoBehaviour
    {
        [Inject] private DefaulMovementInput _movementInput;

        private Vector2 _newCameraRotation;      
        private Vector2 _inputMovement;

        private void Awake()
        {
            InitializeInputSystem();
        }
        private void InitializeInputSystem()
        {
            _movementInput.Player.Movement.performed += e => _inputMovement = e.ReadValue<Vector2>();
            _movementInput.Enable();
        }

    }
}
