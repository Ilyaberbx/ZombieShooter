using System;
using UnityEngine;
using Zenject;

namespace FPS

{
    public class Player : InGameBehaviour, IPlayer
    {
        [Inject] protected WeaponInput _weaponInput;

        public event Action OnDied;
        public UnitDamageHandler UnitDamageHandler { get; private set; }
        public UnitHealthHandler UnitHealthHandler { get; private set; }
        public PlayerWeaponLauncher WeaponLauncher { get; private set; }
        public int Health => _health;

        [SerializeField] private int _health;

        private void Awake()
        {
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            UnitHealthHandler = new UnitHealthHandler();
            UnitDamageHandler = new UnitDamageHandler();
            WeaponLauncher = GetComponentInChildren<PlayerWeaponLauncher>();
            WeaponLauncher.Initialize(this);
            UnitHealthHandler.Inititalize(this);
            UnitDamageHandler.Initialize(this);
        }
        private void OnEnable() => _weaponInput.Weapon.FirePressed.performed += e => Attack();
        private void OnDisable() => _weaponInput.Weapon.FirePressed.performed -= e => Attack();

        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        public void Attack() => WeaponLauncher.PerformAttack();

        public void Die()
        {
            OnDied?.Invoke();
        }
    }
}
