using UnityEngine;

namespace FPS
{
    public interface IWeaponVisitor
    {
        void Visit(Pistol weapon,RaycastHit hit);
        void Visit(Rifle weapon, RaycastHit hit);
    }
}
