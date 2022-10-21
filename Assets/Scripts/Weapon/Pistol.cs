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
        public override void StartShooting()
        {
            _isShooting = true;
            _shootEffect.Play();
            _shootEffect.gameObject.SetActive(true);
            OnAttackInvoker(true);
        }
        public override void StopShooting()
        {
            _isShooting = false;
            _shootEffect.Stop();
            _shootEffect.gameObject.SetActive(false);
            OnAttackInvoker(false);
        }

        public override IEnumerator ShootingRoutine()
        {
            StartShooting();

            _inCoolDown = true;

            RaycastHit hit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _shootingRange))
                HandleHit(hit);

            CurrentAmmo--;
            ChangeAmmoCount(CurrentAmmo);

            yield return new WaitForSeconds(_shotsInTime);

            _inCoolDown = false;

            StopShooting();

        }
        private void HandleHit(RaycastHit hit)
        {
            BaseDecal hitMark = _decalsPreset.BulletDecal;
            hitMark.transform.position = hit.point;

            if (hit.transform.TryGetComponent(out IWeaponVisitor visitor))
                visitor.Visit(this, hit);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * _reboundForce);
        }
        public override WeaponType GetWeaponType() => WeaponType.Pistol;
    }
}
