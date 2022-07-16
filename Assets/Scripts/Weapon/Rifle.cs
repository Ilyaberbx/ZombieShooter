using System.Collections;
using UnityEngine;

namespace FPS
{

    public class Rifle : BaseShootableWeapon
    {
        public override void Attack()
        {
            if (!CanShoot()) return;

            if (_shootingRoutine != null) StopCoroutine(_shootingRoutine);

            _shootingRoutine = StartCoroutine(ShootingRoutine());
        }
        public override void StartAttacking()
        {
            _isShooting = true;
            OnAttackedInvoker(_isShooting);
        }
        public override void StopAttacking()
        {
            _isShooting = false;
            OnAttackedInvoker(_isShooting);
        }

        public override IEnumerator ShootingRoutine()
        {
            StartAttacking();

            while (_isShooting && CurrentAmmo > 0)
            {
                _inCoolDown = true;
                _shootEffect.gameObject.SetActive(true);
                _shootEffect.Play();

                RaycastHit hit;
                if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _shootingRange))
                    ApplyHit(hit);

                CurrentAmmo--;
                ChangeAmmoCount(CurrentAmmo);

                yield return new WaitForSeconds(_shotsInTime);

                _shootEffect.Stop();
                _shootEffect.gameObject.SetActive(false);
                _inCoolDown = false;
            }

            StopAttacking();
        }
   
    }
}
