using UnityEngine;
using Zenject;

namespace FPS
{
    public class MainMenuLevelSelectionButton : LevelSelectionButton
    {
        [Inject] private LevelsProvider _levelsProvider;

        private void OnEnable()
        {
            if (_levelsProvider.IsLevelCompleted(_levelId))
                Competed();
        }

        private void Competed() => _button.image.color = Color.green;
    }
}
