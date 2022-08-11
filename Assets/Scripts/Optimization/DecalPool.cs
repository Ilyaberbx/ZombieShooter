using UnityEngine;

namespace FPS
{
    public class DecalPool<T> where T : BaseDecal
    {
        public PoolMono<T> Pool { get; private set; }
        public void Initialize(T prefab, int countInPool, bool autoExpand, Transform parent = null) 
            => Pool = new PoolMono<T>(prefab, countInPool, autoExpand, parent);
    }
}
