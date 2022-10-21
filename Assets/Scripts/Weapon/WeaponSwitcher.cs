using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class WeaponSwitcher : InGameBehaviour
    {
        [Inject] private WeaponInput _weaponInput;

        private int _selectedWeapon;
        private int _previousWeapon;
        private int _selectionMax;
        private float _inputSelection;
        private WeaponLauncher _weaponLauncher;
        private WeaponVault _weaponVault;

        private void Awake() => Inititalize();
        private void OnEnable() => _weaponInput.Weapon.MouseScroll.performed += e => _inputSelection = e.ReadValue<float>();
        private void OnDisable() => _weaponInput.Weapon.MouseScroll.performed -= e => _inputSelection = e.ReadValue<float>();
        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;
        private void Inititalize()
        {
            _weaponLauncher = GetComponent<WeaponLauncher>();
            _weaponVault = new WeaponVault();
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            SelectWeapon();
            CalculateSelectionRange();
        }
        private void Update() => DefaultSelectionLogic();
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
        private void CalculateSelectionRange()
        {
            _selectionMax = 0;
            foreach (Transform weapon in transform)
            {
                if (_weaponVault.IsAvaliable(weapon.GetComponent<IWeapon>().GetWeaponType()))
                    _selectionMax++;
            }
        }
        private void SetSelectedWeapon(IWeapon weapon) => _weaponLauncher.SetWeapon(weapon);

        private void CalculateInputSelection()
        {
            if (_inputSelection > 0f)
                _selectedWeapon = _selectedWeapon >= transform.childCount - 1 ? 0 : _selectedWeapon + 1;
            if (_inputSelection < 0f)
                _selectedWeapon = _selectedWeapon <= transform.childCount - 1 ? 0 : _selectedWeapon - 1;

            _selectedWeapon = Mathf.Clamp(_selectedWeapon, 0, _selectionMax - 1);
        }
    }
}
