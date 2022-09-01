using UnityEngine;
using Zenject;

namespace FPS
{
    public class LevelProgress : MonoBehaviour
    {
        [Inject] private KilledEnemiesDisplayer _killedEnemiesDisplayer;
        [Inject] private GameExodusController _gameExodusController;
        [SerializeField] private int _needEnemyCount;

        public int NeedEnemyCount => _needEnemyCount;

        private int _currentKilledEnemyCount = 0;

        private void OnEnable() => BaseEnemy.OnDied += OnEnemyDie;
        private void OnDisable() => BaseEnemy.OnDied -= OnEnemyDie;

        private void OnEnemyDie()
        {
            _killedEnemiesDisplayer.Add();

            _currentKilledEnemyCount++;

            if (_currentKilledEnemyCount < _needEnemyCount) return;

            _gameExodusController.Win();
        }
    }
}