using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace FPS
{
    public abstract class BaseShootableWeapon : InGameBehaviour, IWeapon
    {
        [Inject] protected WeaponInput _weaponInput;
        [Inject] protected DecalPreset _decalsPreset;
        public abstract void Attack();

        public abstract void StartShooting();

        public abstract void StopShooting();

        public abstract IEnumerator ShootingRoutine();

        public event Action<bool> OnAttacking;
        public event Action<int> OnAmmoCountChanged;
        public int CurrentAmmo { get; protected set; }
        public int Damage => _damage;

        [Header("Weapon Settings")]
        [Range(0.1f, 2)]
        [SerializeField] protected float _shotsInTime;
        [SerializeField] protected float _shootingRange;
        [SerializeField] private int _maxAmmoCount;
        [SerializeField] private float _reloadTime;
        [SerializeField] private bool _autoGun;
        [SerializeField] private int _damage;
        [SerializeField] protected int _reboundForce;

        [Header("References")]
        [SerializeField] protected Camera _camera;
        [SerializeField] protected ParticleSystem _shootEffect;

        protected Coroutine _shootingRoutine;
        protected bool _inCoolDown;
        protected bool _isShooting;
        protected PlayerMovement _playerMovement;
        protected Gravity _gravity;

        private ShootableWeaponAnimator _weaponAnimator;

        private void Start() => ChangeAmmoCount(_maxAmmoCount);
        private void Awake() => Initialize();
        private void OnEnable()
        {
            _playerMovement.OnJumped += StopShooting;

            if (_autoGun)
                _weaponInput.Weapon.FireReleased.performed += e => StopShooting();

            _weaponInput.Weapon.Reload.performed += e => Reload();
        }
        private void OnDisable()
        {
            _playerMovement.OnJumped -= StopShooting;         

            if (_autoGun)
                _weaponInput.Weapon.FireReleased.performed -= e => StopShooting();

            _weaponInput.Weapon.Reload.performed -= e => Reload();
            DisactivateWeapon();
        }
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        private void Initialize()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
            _gravity = GetComponentInParent<Gravity>();
            _weaponAnimator = GetComponentInChildren<ShootableWeaponAnimator>();
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            _shootEffect.Stop();
            _shootEffect.gameObject.SetActive(false);
            _weaponInput.Enable();
        }

        protected void OnAttackInvoker(bool isShooting) => OnAttacking?.Invoke(isShooting);
        protected void ChangeAmmoCount(int ammoCount)
        {
            CurrentAmmo = ammoCount;
            OnAmmoCountChanged?.Invoke(CurrentAmmo);
        }
        protected bool CanShoot()
        {
            if (_inCoolDown) return false;

            if (CurrentAmmo <= 0) return false;

            return true;
        }    
        private void PlayerSprinting(bool isSprinting) { if (isSprinting) StopShooting(); }

        private void Reload()
        {
            if (_inCoolDown) return;

            if (CurrentAmmo == _maxAmmoCount) return;

            StartCoroutine(ReloadingRoutine());
        }
        private IEnumerator ReloadingRoutine()
        {
            _inCoolDown = true;

            _weaponAnimator.CalculateReloading(true);

            yield return new WaitForSeconds(_reloadTime);

            _weaponAnimator.CalculateReloading(false);

            CurrentAmmo = _maxAmmoCount;
            ChangeAmmoCount(CurrentAmmo);
            _inCoolDown = false;
        }
        private void DisactivateWeapon()
        {
            if (_shootingRoutine != null) StopCoroutine(_shootingRoutine);
            _shootEffect.Stop();
            _shootEffect.gameObject.SetActive(false);
            _inCoolDown = false;
        }

        protected override void OnGameStateChanged(GameState newState)
        {
            if(newState == GameState.Pause)
            StopShooting();
        }
    }
}
