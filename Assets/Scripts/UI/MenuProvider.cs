using System.Collections.Generic;
using UnityEngine;
namespace FPS
{
    public class MenuProvider : MonoBehaviour
    {
        [SerializeField] private List<MenuChild> _menus;    
        private void SetMenuState(MenuState state)
        {
            Debug.Log(this);
            foreach (var menu in _menus)
                menu.gameObject.SetActive(menu.MenuState == state);
        }
        public void LevelSeletion() => SetMenuState(MenuState.LevelSelection);
        public void MainMenu() => SetMenuState(MenuState.MainMenu);
        public void Shop() => SetMenuState(MenuState.Shop);
        public void PauseSettings() => SetMenuState(MenuState.Pause);
        public void GameOver() => SetMenuState(MenuState.GameOver);
        public void GamePlay() => SetMenuState(MenuState.GamePlay);
        public void GameWin() => SetMenuState(MenuState.GameWin);

    }
}
