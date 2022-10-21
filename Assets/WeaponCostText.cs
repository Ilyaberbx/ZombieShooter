using UnityEngine;
using TMPro;

namespace FPS
{
    public class WeaponCostText : MonoBehaviour
    {
        [SerializeField] private WeaponShopHolder _weaponShopHolder;
        private TextMeshProUGUI _text;
        private void Awake() => _text = GetComponent<TextMeshProUGUI>();
        private void OnEnable() => _weaponShopHolder.OnWeaponChanged += DisplayCost;
        private void OnDisable() => _weaponShopHolder.OnWeaponChanged -= DisplayCost;
        public void DisplayCost(WeaponShopItem currentWeaponCost) => _text.text = "Cost: " + currentWeaponCost.Cost;
    }
}
