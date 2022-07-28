using TMPro;
using UnityEngine;

namespace FPS
{
    public class ShootableWeaponCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _ammoCount;
        private BaseShootableWeapon _weapon;

        private void Awake() => _weapon = GetComponentInParent<BaseShootableWeapon>();
        private void OnEnable() => _weapon.OnAmmoCountChanged += DisplayAmmoCount;
        private void OnDisable() => _weapon.OnAmmoCountChanged -= DisplayAmmoCount;
        private void DisplayAmmoCount(int ammo) => _ammoCount.text = "Ammo: " + ammo;
    }
}
