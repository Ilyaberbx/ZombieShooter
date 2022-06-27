using UnityEngine;

namespace FPS
{
    public class MovementSettings : MonoBehaviour
    {
        [SerializeField] private int _walkingForwardSpeed;
        [SerializeField] private int _walkingBackWardSpeed;
        [SerializeField] private int _walkingStrafeSpeed;
        [SerializeField] private int _jumpingForce;

        public int WalkingForwardSpeed => _walkingForwardSpeed;
        public int WalkingBackWardSpeed => _walkingBackWardSpeed;
        public int WalkingStrafeSpeed => _walkingStrafeSpeed;
        public int JumpingForce => _jumpingForce;
    }
}
