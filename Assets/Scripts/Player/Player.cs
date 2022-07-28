using System;
using UnityEngine;
using Zenject;

namespace FPS

{
    public class Player : GamePlayBehaviour, IPlayer
    {

        [Inject] protected WeaponInput _weaponInput;
        public UnitDamageHandler UnitDamageHandler => GetComponent<UnitDamageHandler>();
        public UnitHealth UnitHealth => GetComponent<UnitHealth>();
        public WeaponLauncher WeaponLauncher { get; private set; }

        public event Action OnDied;

        private void Awake()
        {
            WeaponLauncher = GetComponentInChildren<WeaponLauncher>();
            WeaponLauncher.Initialize(this);
            UnitHealth.Inititalize(this);
            UnitDamageHandler.Initialize(this);
        }
        private void OnEnable()
        {
            _weaponInput.Weapon.FirePressed.performed += e => Attack();
            GameStateController.OnGameStateChanged += OnGameStateChanged;
        }
        private void OnDisable()
        {
            _weaponInput.Weapon.FirePressed.performed -= e => Attack();
            GameStateController.OnGameStateChanged -= OnGameStateChanged;
        }
        public void Attack() => WeaponLauncher.PerformAttack();

        public void Die()
        {
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}
