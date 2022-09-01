using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace FPS
{
    public class SceneProvider 
    {
        private LevelsProvider _levelsProvider;

        private readonly int MAINMENU = 0;
        private readonly string LOADING = "Loading";

        public SceneProvider(LevelsProvider levelsProvider) => _levelsProvider = levelsProvider;
        public void Restart() => OpenLevel(_levelsProvider.CurrentLevel.ID);
        public void Menu() => OpenLevel(MAINMENU);
        public void OpenLevel(int id)
        {
            _levelsProvider.NextLevel.ID = id;
            SceneManager.LoadScene(LOADING);
            new Loading(_levelsProvider, id);
        }
    }
}
