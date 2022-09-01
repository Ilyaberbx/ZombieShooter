using UnityEngine;
using Zenject;

namespace FPS
{
    public class WeaponShop : MonoBehaviour, IShop<WeaponType>
    {
        [Inject] public Wallet PlayerWallet { get; }

        public void Buy(WeaponType weaponType, int cost)
        {
            if(PlayerWallet.Coins >= cost)
            {
                //Realisation;
            }
        }
    }
}
