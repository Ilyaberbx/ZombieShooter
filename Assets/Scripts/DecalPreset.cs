using UnityEngine;
using Zenject;

namespace FPS
{
    public class DecalPreset : MonoBehaviour
    {
        [Inject] private DecalPoolsProvider _decalPools;
        public BloodDecal BloodDecal => _decalPools.BloodDecalPool.Pool.GetFreeElement();
        public BulletDecal BulletDecal => _decalPools.BulletDecalPool.Pool.GetFreeElement();
        public BrainDecal BrainDecal => _decalPools.BrainDecalPool.Pool.GetFreeElement();
    }
}

