using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class DefaultZombie : GamePlayBehaviour, IEnemy
    {
        [Inject] private Player _player;

        public event Action OnDied;
        public UnitDamageHandler UnitDamageHandler { get; private set; }
        public UnitHealth UnitHealth { get; private set; }
        public NavMeshEnemyChaser TargetChaser { get; private set; }
        public EnemyAnimator Animator { get; private set; }
        public IWeapon Weapon { get; private set; }
        public int TimeBeforeChase => _timeBeforeChase * 1000;// TO CONVERT IN SEC

        [Header("EnemySettings")]
        [SerializeField] private int _timeBeforeChase;
        [SerializeField] private float _timeBeforeDisappear;
        [SerializeField] private Transform _attackPosition;
        [SerializeField] private float _distanceToAttack;

        private Coroutine _dyingRoutine;

        private void Awake()
        {
            Initialize();
            AwakeBeforeChasing();
        }

        private void OnEnable() => GameStateController.OnGameStateChanged += OnGameStateChanged;

        private void OnDisable() => GameStateController.OnGameStateChanged -= OnGameStateChanged;

        private void Initialize()
        {
            UnitHealth = GetComponent<UnitHealth>();
            UnitDamageHandler = GetComponent<UnitDamageHandler>();
            TargetChaser = GetComponent<NavMeshEnemyChaser>();
            Animator = GetComponentInChildren<EnemyAnimator>();
            Weapon = GetComponent<IWeapon>();
            UnitHealth.Inititalize(this);
            UnitDamageHandler.Initialize(this);
            TargetChaser.Initialize(_player);
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
        public async void Attack()
        {
            SetChasing(false);
            Animator.AnimateAttacking();

            Weapon.Attack();

            await Task.Delay(TimeBeforeChase);
            SetChasing(true);
        }
        private void SetChasing(bool isChasing)
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
