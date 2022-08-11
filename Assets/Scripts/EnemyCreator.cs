using UnityEngine;
using Zenject;

namespace FPS
{
    public class EnemyCreator : InGameBehaviour
    {
        [Inject] private DefaulZombieFactory _defaulZombieFactory;
        [Inject] private KilledEnemiesDisplayer _killedEnemy;

        [Header("Creator Settings")]
        [SerializeField] private int _maxEnemyCount;
        [SerializeField] private float _enemySpawnRate;
        [SerializeField] private EnemySpawnPosition[] _spawnPositions;

        [Header("Prefabs")]
        [SerializeField] private DefaultZombie _defaultZombie;

        private int _currentEnemyCount;
        private float _timeRate;

        private void Awake() => Initialize();
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;

        private void Initialize()
        {
            _defaulZombieFactory.Init(_defaultZombie);
            GameStateController.OnGameStateChanged += OnGameStateChanged;
        }
        private BaseEnemy SpawnLogic()
        {
            _currentEnemyCount++;

            Vector3 pos = _spawnPositions[UnityEngine.Random.Range(0, _spawnPositions.Length)].transform.position;

            var enemy = _defaulZombieFactory.Create(pos, null);

            enemy.OnDied += OnCreatedEnemyDie;

            _timeRate += _enemySpawnRate;

            return enemy;
        }
        private void OnCreatedEnemyDie()
        {
            _currentEnemyCount--;
            _killedEnemy.Add();
        }
        private void Update()
        {
            if (_currentEnemyCount <= _maxEnemyCount && Time.time >= _timeRate)
                SpawnLogic();
        }
    }
}
