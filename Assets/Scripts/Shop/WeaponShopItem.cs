using UnityEngine;

namespace FPS
{
    public class WeaponShopItem : MonoBehaviour
    {
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private int _cost;
        public WeaponType WeaponType => _weaponType;
        public int Cost => _cost;
    }
}
