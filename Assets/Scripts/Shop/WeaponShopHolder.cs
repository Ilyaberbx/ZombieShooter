using System;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class WeaponShopHolder : MonoBehaviour
    {
        public Action<WeaponShopItem> OnWeaponChanged;
        public WeaponShopItem CurrentWeapon => _currentWeapon;

        private WeaponShopItem _currentWeapon;
        private int _currentWeaponIndex;
        private int _selectionMax;
        private List<Transform> _weapons = new List<Transform>();

        private void Awake() => Inititalize();
        private void Inititalize()
        {
            _selectionMax = transform.childCount - 1;
            _currentWeaponIndex = 0;

            foreach (Transform weapon in transform)
                _weapons.Add(weapon);

            CalculateWeapon();
        }
        public void NextWeapon()
        {
            _currentWeaponIndex++;
            CalculateWeapon();
        }
        public void PreviousWeapon()
        {
            _currentWeaponIndex--;
            CalculateWeapon();
        }
        private void CalculateWeapon()
        {
            _currentWeaponIndex = Mathf.Clamp(_currentWeaponIndex, 0, _selectionMax);
            foreach (Transform weapon in _weapons)
                weapon.gameObject.SetActive(false);

            _currentWeapon = _weapons[_currentWeaponIndex].GetComponent<WeaponShopItem>();
            _currentWeapon.gameObject.SetActive(true);

            OnWeaponChanged?.Invoke(_currentWeapon);
        }

    }
}
