using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace FPS
{
    public abstract class BaseEnemy : InGameBehaviour,IUnit
    {
        [Inject] private Player _player;
        public NavMeshEnemyChaser TargetChaser { get; private set; }
        public EnemyAnimator Animator { get; private set; }
        public IWeapon Weapon { get; private set; }
        public int TimeBeforeChase => _timeBeforeChase * 1000;// TO CONVERT IN SEC
        public int Health => _health;
        public UnitDamageHandler UnitDamageHandler { get; private set; }
        public UnitHealthHandler UnitHealthHandler { get; private set; }

        public event Action OnDied;

        [Header("EnemySettings")]
        [SerializeField] private int _timeBeforeChase;
        [SerializeField] private float _timeBeforeDisappear;
        [SerializeField] private Transform _attackPosition;
        [SerializeField] private float _distanceToAttack;
        [SerializeField] private int _health;

        private Coroutine _dyingRoutine;
        private void Awake()
        {
            Initialize();
            AwakeBeforeChasing();
        }

        private void OnDestroy()
        {
            GameStateController.OnGameStateChanged -= OnGameStateChanged;
        }

        private void Initialize()
        {
            UnitHealthHandler = new UnitHealthHandler();
            UnitDamageHandler = new UnitDamageHandler();
            TargetChaser = GetComponent<NavMeshEnemyChaser>();
            Animator = GetComponentInChildren<EnemyAnimator>();
            Weapon = GetComponent<IWeapon>();
            UnitHealthHandler.Inititalize(this);
            UnitDamageHandler.Initialize(this);
            TargetChaser.Initialize(_player);
            GameStateController.OnGameStateChanged += OnGameStateChanged;
        }
        private async void AwakeBeforeChasing()
        {
            TargetChaser.enabled = false;
            await Task.Delay(TimeBeforeChase);
            SetChasing(true);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) <= _distanceToAttack)
                Attack();
        }
        public abstract void Attack();
        protected void SetChasing(bool isChasing)
        {
            if (TargetChaser != null)
            {
                TargetChaser.SetMoveable(isChasing);
                TargetChaser.enabled = isChasing;
                Animator.AnimateChasing(isChasing);
            }
        }

        public void Die()
        {
            if (_dyingRoutine != null) return;
            _dyingRoutine = StartCoroutine(DieRoutine());
        }

        private IEnumerator DieRoutine()
        {
            Animator.AnimateDying();

            OnDied?.Invoke();
            SetChasing(false);

            yield return new WaitForSeconds(_timeBeforeDisappear);
            Destroy(gameObject);
        }

    }
}
