using UnityEngine;

namespace FPS
{
    public class ReloadableWeaponAnimator : WeaponAnimator
    {
        public void CalculateReloading(bool isReloading) => _animator.SetBool(IS_RELOADING, isReloading);
    }
}
