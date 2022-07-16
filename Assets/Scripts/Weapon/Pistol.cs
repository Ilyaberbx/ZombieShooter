using System.Collections;
using UnityEngine;

namespace FPS
{
    public class Pistol : BaseShootableWeapon
    {
        public override void Attack()
        {
            if (!CanShoot()) return;

            _shootingRoutine = StartCoroutine(ShootingRoutine());
        }

        public override void StartAttacking()
        {
            _isShooting = true;
            _shootEffect.Play();
            _shootEffect.gameObject.SetActive(true);
            OnAttackedInvoker(_isShooting);
        }
        public override void StopAttacking()
        {
            _isShooting = false;
            _shootEffect.Stop();
            _shootEffect.gameObject.SetActive(false);
            OnAttackedInvoker(_isShooting);
        }

        public override IEnumerator ShootingRoutine()
        {
            StartAttacking();

            _inCoolDown = true;

            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _shootingRange))
                ApplyHit(hit);

            CurrentAmmo--;
            ChangeAmmoCount(CurrentAmmo);

            yield return new WaitForSeconds(_shotsInTime);

            _inCoolDown = false;

            StopAttacking();

        }

    }
}
