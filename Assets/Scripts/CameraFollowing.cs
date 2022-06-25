using UnityEngine;
using Zenject;

namespace FPS
{
    public class CameraFollowing : MonoBehaviour
    {
        [Inject] private DefaulMovementInput _movementInput;
        private Vector2 _inputView;

        private void Awake() => InitializeInputSystem();
        private void InitializeInputSystem()
        {
            _movementInput.Player.View.performed += e => _inputView = e.ReadValue<Vector2>();
        }
    }
}
