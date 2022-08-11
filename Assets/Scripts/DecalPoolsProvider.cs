using UnityEngine;

namespace FPS
{
    public class DecalPoolsProvider : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int _poolsVolume;
        [SerializeField] private BulletDecal _bulletDecal;
        [SerializeField] private BloodDecal _bloodDecal;
        [SerializeField] private BrainDecal _brainDecal;

        public DecalPool<BulletDecal> BulletDecalPool { get; private set; }
        public DecalPool<BloodDecal> BloodDecalPool { get; private set; }
        public DecalPool<BrainDecal> BrainDecalPool { get; private set; }

        private void Awake() => InitializePools(_poolsVolume);
        private void InitializePools(int poolVolume)
        {
            BulletDecalPool = new DecalPool<BulletDecal>();
            BloodDecalPool = new DecalPool<BloodDecal>();
            BrainDecalPool = new DecalPool<BrainDecal>();
            BloodDecalPool.Initialize(_bloodDecal, poolVolume, true, transform);
            BulletDecalPool.Initialize(_bulletDecal, poolVolume, true, transform);
            BrainDecalPool.Initialize(_brainDecal, poolVolume, true, transform);
        }
    }
}