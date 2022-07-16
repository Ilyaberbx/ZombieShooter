using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace FPS
{
    public abstract class BaseShootableWeapon : GamePlayBehaviour, IWeapon
    {
        public abstract void Attack();

        public abstract void StartAttacking();

        public abstract void StopAttacking();

        public abstract IEnumerator ShootingRoutine();

        [Inject] protected WeaponInput _weaponInput;

        public event Action<bool> OnAttacked;
        public event Action<int> OnAmmoCountChanged;
        public int CurrentAmmo { get; protected set; }

        [Header("Weapon Settings")]
        [Range(0.1f, 2)]
        [SerializeField] protected float _shotsInTime;
        [SerializeField] protected float _shootingRange;
        [SerializeField] private int _maxAmmoCount;
        [SerializeField] private float _reloadTime;
        [SerializeField] private bool _autoGun;

        [Header("References")]
        [SerializeField] protected Camera _camera;
        [SerializeField] private HitMarkPool _hitMarkPool;
        [SerializeField] private HitMark _hitMarkPrefab;
        [SerializeField] protected ParticleSystem _shootEffect;

        protected Coroutine _shootingRoutine;
        protected bool _inCoolDown;
        protected bool _isShooting;
        protected PlayerMovement _playerMovement;
        protected Gravity _gravity;

        private ReloadableWeaponAnimator _weaponAnimator;

        private void Start() => ChangeAmmoCount(_maxAmmoCount);
        private void Awake() => Initialize();
        private void OnEnable()
        {
            _playerMovement.OnJumped += StopAttacking;
            _playerMovement.OnSprintingToggled += PlayerSprinting;
            _weaponInput.Weapon.FirePressed.performed += e => Attack();

            if (_autoGun)
                _weaponInput.Weapon.FireReleased.performed += e => StopAttacking();

            _weaponInput.Weapon.Reload.performed += e => Reload();
        }
        private void OnDisable()
        {
            _playerMovement.OnJumped -= StopAttacking;
            _playerMovement.OnSprintingToggled -= PlayerSprinting;
            _weaponInput.Weapon.FirePressed.performed -= e => Attack();

            if (_autoGun)
                _weaponInput.Weapon.FireReleased.performed -= e => StopAttacking();

            _weaponInput.Weapon.Reload.performed -= e => Reload();
            DisactivateWeapon();
        }
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        protected void OnAttackedInvoker(bool isShooting) => OnAttacked?.Invoke(isShooting);
        protected void ChangeAmmoCount(int ammoCount)
        {
            CurrentAmmo = ammoCount;
            OnAmmoCountChanged?.Invoke(CurrentAmmo);
        }
        protected bool CanShoot()
        {
            if (_inCoolDown) return false;

            if (CurrentAmmo <= 0) return false;

            if (_playerMovement.IsSprinting || !_gravity.IsGrounded)
            {
                _isShooting = false;
                return false;
            }

            return true;
        }
        protected void ApplyHit(RaycastHit hit)
        {
            var hitMark = _hitMarkPool.Pool.GetFreeElement();
            hitMark.transform.position = hit.point;
        }
        private void Initialize()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
            _gravity = GetComponentInParent<Gravity>();
            _weaponAnimator = GetComponentInChildren<ReloadableWeaponAnimator>();
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            _hitMarkPool.Initialize(_hitMarkPrefab, _maxAmmoCount, false);
            _shootEffect.Stop();
            _shootEffect.gameObject.SetActive(false);
            _weaponInput.Enable();
        }

        private void PlayerSprinting(bool isSprinting) { if (isSprinting) StopAttacking(); }

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

    }
}
