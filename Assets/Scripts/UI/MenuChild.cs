using UnityEngine;

namespace FPS
{
    public class MenuChild : MonoBehaviour
    {
        [SerializeField] private MenuState _menuState;
        public MenuState MenuState => _menuState;
    }
}
