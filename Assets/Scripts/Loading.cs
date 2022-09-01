using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace FPS
{
    public class Loading
    {
        private LevelsProvider _levelsProvider;
        private readonly int _loadingDuration = 3000;

        public async void Load(int nextLevelID)
        {
            await Task.Delay(_loadingDuration);
            _levelsProvider.CurrentLevel.ID = nextLevelID;
            SceneManager.LoadScene(nextLevelID);
        }
        public Loading(LevelsProvider levelsProvider, int nextLevelID)
        {
            _levelsProvider = levelsProvider;
            Load(nextLevelID);
        }
    }
}
