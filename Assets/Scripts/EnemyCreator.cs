using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class EnemyCreator : InGameBehaviour
    {
        [Inject] private DefaultZombieFactory _defaulZombieFactory;

        [Header("Creator Settings")]
        [SerializeField] private int _maxEnemyCount;
        [SerializeField] private float _enemySpawnRate;
        [SerializeField] private UnitSpawnPoint[] _spawnPositions;

        [Header("Prefabs")]
        [SerializeField] private DefaultZombie _defaultZombie;

        private int _currentEnemyCount;
        private float _timeRate;
        private List<BaseEnemy> _enemyCreated = new List<BaseEnemy>();

        private void Awake() => Initialize();
        private void OnDestroy()
        {
            GameStateController.OnGameStateChanged -= OnGameStateChanged;
        }

        private void Initialize()
        {       
            _enemyCreated.Clear();
            _enemySpawnRate = 2f;
            _timeRate = Time.time + _enemySpawnRate;
            Debug.Log(_currentEnemyCount);
            _defaulZombieFactory.Init(_defaultZombie);
            GameStateController.OnGameStateChanged += OnGameStateChanged;
        }
        private BaseEnemy SpawnLogic()
        {
            _currentEnemyCount++;

            Vector3 pos = _spawnPositions[UnityEngine.Random.Range(0, _spawnPositions.Length)].transform.position;

            BaseEnemy enemy = _defaulZombieFactory.Create(pos, null);

            Debug.Log("Create");

            _timeRate += _enemySpawnRate;

            return enemy;
        }

        private void Update()
        {
            if (_currentEnemyCount <= _maxEnemyCount && Time.time >= _timeRate)
                _enemyCreated.Add(SpawnLogic());
        }

    }
}
