using UnityEngine;
using Zenject;

namespace FPS
{
    public class WeaponShop : MonoBehaviour, IShop
    {
        [Inject] public Wallet PlayerWallet { get; }
        [SerializeField] private WeaponShopHolder _weaponShopHolder;
        private WeaponVault _weaponVault;

        private void Awake() => _weaponVault = new WeaponVault();

        public void Buy()
        {
            if (PlayerWallet.Coins < _weaponShopHolder.CurrentWeapon.Cost) return;

            if (_weaponVault.IsAvaliable(_weaponShopHolder.CurrentWeapon.WeaponType)) return;

            PlayerWallet.SubtractCoins(_weaponShopHolder.CurrentWeapon.Cost);
            _weaponVault.SetAvaliable(_weaponShopHolder.CurrentWeapon.WeaponType);
        }
    }
}
