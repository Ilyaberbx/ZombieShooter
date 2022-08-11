using UnityEngine;

namespace FPS
{
    public class GameOverCanvas : MonoBehaviour 
    {
        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}

