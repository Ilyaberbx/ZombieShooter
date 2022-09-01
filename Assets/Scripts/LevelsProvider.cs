using UnityEngine;

namespace FPS
{
    [CreateAssetMenu]
    public class LevelsProvider : ScriptableObject
    {
        [SerializeField] private Level[] _allLevels;

        private Level _currentLevel;
        private Level _nextLevel;

        public Level CurrentLevel => _currentLevel;
        public Level NextLevel => _nextLevel;

        public bool IsLevelCompleted(int id)
        {
            for (int i = 0; i < _allLevels.Length; i++)
            {
                if (_allLevels[i].ID == id)
                    return _allLevels[i].IsCompleted;
            }
            return false;
        }

        public void SetCurrentLevel(int id)
        {
            for (int i = 0; i < _allLevels.Length; i++)
            {
                if (_allLevels[i].ID == id)
                    _currentLevel = _allLevels[i];
            }
        }
        public void SetNextLevel(int id)
        {
            for (int i = 0; i < _allLevels.Length; i++)
            {
                if (_allLevels[i].ID == id)
                    _nextLevel = _allLevels[i];
            }
        }

        public void CompleteLevel() => PlayerPrefs.SetInt(_currentLevel.ID.ToString(), 1);
    
    }
}