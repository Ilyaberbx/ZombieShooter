using TMPro;
using UnityEngine;
using Zenject;

namespace FPS
{
    public class WeaponAttributesDisplayer : MonoBehaviour
    {
        [Inject] private GamePlayCanvas _gamePlayCanvas;

        private BaseShootableWeapon _weapon;

        private void Awake() => _weapon = GetComponentInParent<BaseShootableWeapon>();
        private void OnEnable() => _weapon.OnAmmoCountChanged += _gamePlayCanvas.DisplayAmmoCount;
        private void OnDisable() => _weapon.OnAmmoCountChanged -= _gamePlayCanvas.DisplayAmmoCount;
    }
}
