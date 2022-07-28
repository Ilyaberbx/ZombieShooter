using UnityEngine;
using Zenject;

namespace FPS
{
    public class WeaponSwitcher : GamePlayBehaviour
    {
        [Inject] private WeaponInput _weaponInput;

        public float InputSelection
        {
            get { return _inputSelection; }
            set
            {
                if (_playerMovement.IsSprinting) return;
                _inputSelection = value;
                DefaultSelectionLogic();
            }
        }

        private int _selectedWeapon = 0;
        private int _previousWeapon;
        private float _inputSelection;
        private PlayerMovement _playerMovement;
        private WeaponLauncher _weaponLauncher;

        private void Awake() => Inititalize();
        private void OnEnable() => _weaponInput.Weapon.MouseScroll.performed += e => InputSelection = e.ReadValue<float>();
        private void OnDisable() => _weaponInput.Weapon.MouseScroll.performed -= e => InputSelection = e.ReadValue<float>();
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        private void Inititalize()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
            _weaponLauncher = GetComponent<WeaponLauncher>();
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            SelectWeapon();
        }
        private void DefaultSelectionLogic()
        {
            _previousWeapon = _selectedWeapon;

            CalculateInputSelection();

            if (_previousWeapon != _selectedWeapon)
                SelectWeapon();
        }
        private void SelectWeapon()
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                weapon.gameObject.SetActive(i == _selectedWeapon);
                i++;
                if (weapon.gameObject.activeInHierarchy)
                    SetSelectedWeapon(weapon.GetComponent<IWeapon>());
            }

        }
        private void SetSelectedWeapon(IWeapon weapon)
        {
            _weaponLauncher.SetWeapon(weapon);
        }

        private void CalculateInputSelection()
        {
            if (_inputSelection > 0f)
                _selectedWeapon = _selectedWeapon >= transform.childCount - 1 ? 0 : _selectedWeapon + 1;
            if (_inputSelection < 0f)
                _selectedWeapon = _selectedWeapon <= transform.childCount - 1 ? 0 : _selectedWeapon - 1;
        }
    }
}
