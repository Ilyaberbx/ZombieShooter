using UnityEngine;

namespace FPS
{
    public class HitMarkPool : MonoBehaviour
    {
        public PoolMono<HitMark> Pool { get; private set; }
        public void Initialize(HitMark prefab, int countInPool,bool autoExpand) => Pool = new PoolMono<HitMark>(prefab, countInPool, autoExpand, transform);
    }
}
