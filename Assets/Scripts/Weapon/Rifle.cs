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
        public override void StartShooting()
        {
            _isShooting = true;
            OnAttackInvoker(_isShooting);
        }
        public override void StopShooting()
        {
            _isShooting = false;
            OnAttackInvoker(_isShooting);
        }

        public override IEnumerator ShootingRoutine()
        {
            StartShooting();

            while (_isShooting && CurrentAmmo > 0)
            {
                _inCoolDown = true;
                _shootEffect.gameObject.SetActive(true);
                _shootEffect.Play();

                RaycastHit hit;

                if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _shootingRange))
                    HandleHit(hit);

                CurrentAmmo--;
                ChangeAmmoCount(CurrentAmmo);

                yield return new WaitForSeconds(_shotsInTime);

                _shootEffect.Stop();
                _shootEffect.gameObject.SetActive(false);
                _inCoolDown = false;
            }

            StopShooting();
        }
        private void HandleHit(RaycastHit hit)
        {
            BulletDecal hitMark = _decalsPreset.BulletDecal;
            hitMark.transform.position = hit.point;

            if (hit.transform.TryGetComponent(out IWeaponVisitor visitor))
                visitor.Visit(this,hit);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * _reboundForce);
        }

    }
}
