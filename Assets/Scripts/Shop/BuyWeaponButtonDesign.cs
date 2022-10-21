using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FPS
{
    public class BuyWeaponButtonDesign : MonoBehaviour
    {
        [Inject] private Wallet _wallet;
        [SerializeField] private WeaponShopHolder _weaponShopHolder;
        public Button Button { get; private set; }
        private WeaponVault _weaponVault;

        private void Awake()
        {
            Button = GetComponent<Button>();
            _weaponVault = new WeaponVault();
            CalculateIsEnoughMoneyOrIsBought(_weaponShopHolder.CurrentWeapon);
        }
        private void OnEnable() => _weaponShopHolder.OnWeaponChanged += CalculateIsEnoughMoneyOrIsBought;
        private void OnDisable() => _weaponShopHolder.OnWeaponChanged -= CalculateIsEnoughMoneyOrIsBought;
        private void CalculateIsEnoughMoneyOrIsBought(WeaponShopItem currentWeapon)
        {
            if (currentWeapon.Cost > _wallet.Coins)
            {
                Button.image.color = Color.gray;
                return;
            }

            if(_weaponVault.IsAvaliable(currentWeapon.WeaponType))
            {
                Button.image.color = Color.green;
                return;
            }

            Button.image.color = Color.white;
        }
    }
}
