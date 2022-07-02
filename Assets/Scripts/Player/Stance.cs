using UnityEngine;


namespace FPS
{
    [System.Serializable]
    public class Stance 
    {
        [SerializeField] private CapsuleCollider _stanceCollider;
        [SerializeField] private float _cameraHeight;
        [SerializeField] private PlayerStance _stanceType;
        public CapsuleCollider StanceCollider => _stanceCollider;
        public float CameraHeight  => _cameraHeight;
        public PlayerStance StanceType  => _stanceType;
    }
}
