using UnityEngine;

namespace FPS
{
    [System.Serializable]
    public class MovementSettings : MonoBehaviour
    {
        [SerializeField] private int _walkingForwardSpeed;
        [SerializeField] private int _walkingStrafeSpeed;
        [SerializeField] private int _jumpingForce;
        [Range(1, 3)]
        [SerializeField] private float _sprintingSpeedCoefficient;
        [Range(0.1f, 1)]
        [SerializeField] private float _crouchingSpeedCoefficient;
        [Range(0.1f, 1)]
        [SerializeField] private float _proneSpeedUpCoefficient;
        [SerializeField] private float _minVelocityToSprint;

        public int WalkingForwardSpeed => _walkingForwardSpeed;
        public int WalkingStrafeSpeed => _walkingStrafeSpeed;
        public int JumpingForce => _jumpingForce;
        public float SprintingSpeedCoefficient => _sprintingSpeedCoefficient;
        public float ProneSpeedUpCoefficient => _proneSpeedUpCoefficient;
        public float CrouchingSpeedCoefficient => _crouchingSpeedCoefficient;
        public float MinVelocityToSprint => _minVelocityToSprint;
   
    }
}