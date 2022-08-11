using TMPro;
using UnityEngine;

namespace FPS
{
    public class WeaponAttributesDisplayer : MonoBehaviour
    {
        [SerializeField] private GamePlayCanvas _gamePlayCanvas;

        private BaseShootableWeapon _weapon;

        private void Awake() => _weapon = GetComponentInParent<BaseShootableWeapon>();
        private void OnEnable() => _weapon.OnAmmoCountChanged += _gamePlayCanvas.DisplayAmmoCount;
        private void OnDisable() => _weapon.OnAmmoCountChanged -= _gamePlayCanvas.DisplayAmmoCount;
    }
}
