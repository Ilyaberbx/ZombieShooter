using UnityEngine;
using Zenject;

namespace FPS
{
    public class UnitHitBox : MonoBehaviour, IWeaponVisitor
    {
        [Inject] protected DecalPreset _decals;

        private IUnit _unit;

        private void Awake() => _unit = GetComponentInParent<IUnit>();

        public virtual void Visit(Pistol weapon, RaycastHit hit) => DefaultRayCastVisit(weapon, hit, _decals.BloodDecal, 1);

        public virtual void Visit(Rifle weapon, RaycastHit hit) => DefaultRayCastVisit(weapon, hit, _decals.BloodDecal, 1);

        protected void DefaultRayCastVisit(IWeapon weapon, RaycastHit hit, BaseDecal decal, int damageMultiplier)
        {
            _unit.UnitDamageHandler.ApplyDamage(weapon.Damage * damageMultiplier);

            SpawnBloodDecal(decal, hit.point);
        }
        protected void SpawnBloodDecal(BaseDecal decal, Vector3 point) => decal.transform.position = point;
    }
}
