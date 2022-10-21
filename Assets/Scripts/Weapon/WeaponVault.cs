using UnityEngine;

namespace FPS
{   // 1 - AVALIABLE , 0 - NOT AVALIABLE
    public class WeaponVault
    {
        private readonly string WEAPON = "Weapon";
        public void SetAvaliable(WeaponType weaponType) => PlayerPrefs.SetInt(WEAPON + weaponType.ToString(), 1);
        public bool IsAvaliable(WeaponType weaponType)
        {
            if (IsAvaliableByDefault(weaponType)) return true;

            bool isAvaliable = 1 == PlayerPrefs.GetInt(WEAPON + weaponType.ToString(), 0);
            return isAvaliable;
        }

        private bool IsAvaliableByDefault(WeaponType weaponType) => weaponType == WeaponType.Pistol;
    }
}
