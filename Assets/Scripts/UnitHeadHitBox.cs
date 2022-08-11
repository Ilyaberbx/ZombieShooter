using System;
using UnityEngine;

namespace FPS
{
    public class UnitHeadHitBox : UnitHitBox
    {
        public event Action OnHeadShot;

        public override void Visit(Pistol weapon, RaycastHit hit)
        {
            DefaultRayCastVisit(weapon, hit, _decals.BrainDecal, 3);
            OnHeadShot?.Invoke();
        }
        public override void Visit(Rifle weapon, RaycastHit hit)
        {
            DefaultRayCastVisit(weapon, hit, _decals.BrainDecal,3);
            OnHeadShot?.Invoke();
        }

    }
}
