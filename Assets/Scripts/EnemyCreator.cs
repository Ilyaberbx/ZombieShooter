using UnityEngine;

namespace FPS
{
    public class EnemyCreator : GamePlayBehaviour
    {
        [Header("Factories")]
        [SerializeField] private DefaulZombieFactory _defaulZombieFactory;

        [Header("Creator Settings")]
        [SerializeField] private int _maxEnemyCount;
        [SerializeField] private float _enemySpawnRate;
        [SerializeField] private EnemySpawnPosition[] _spawnPositions; 

        private int _currentEnemyCount;
        private float _timeRate;

        private void OnEnable() => GameStateController.OnGameStateChanged += OnGameStateChanged;
        private void OnDisable() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        private IEnemy SpawnLogic()
        {
            _currentEnemyCount++;

            Vector3 pos = _spawnPositions[UnityEngine.Random.Range(0, _spawnPositions.Length)].transform.position;

            var enemy = _defaulZombieFactory.CreateEnemy(pos, null);

            enemy.OnDied += OnCreatedEnemyDie;

            _timeRate += _enemySpawnRate;

            return enemy;
        }
        private void OnCreatedEnemyDie() => _currentEnemyCount--;
        private void Update()
        {
            if (_currentEnemyCount <= _maxEnemyCount && Time.time >= _timeRate)
                SpawnLogic();
        }
    }
}
