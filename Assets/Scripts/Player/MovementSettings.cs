using UnityEngine;

namespace FPS
{
    public class MovementSettings : MonoBehaviour
    {
        [SerializeField] private int _walkingForwardSpeed;
        [SerializeField] private int _walkingStrafeSpeed;
        [SerializeField] private int _jumpingForce;

        [Range(1, 3)]
        [SerializeField] private float _sprintingSpeedUpCoefficient;
        [Range(0.1f, 1)]
        [SerializeField] private float _crouchingSpeedUpCoefficient;
        [Range(0.1f, 1)]
        [SerializeField] private float _proneSpeedUpCoefficient;

        [SerializeField] private float _minVelocityToSprint;
        public int WalkingForwardSpeed => _walkingForwardSpeed;
        public int WalkingStrafeSpeed => _walkingStrafeSpeed;
        public int JumpingForce => _jumpingForce;
        public float SprintingSpeedUpCoefficient => _sprintingSpeedUpCoefficient;
        public float ProneSpeedUpCoefficient => _proneSpeedUpCoefficient;
        public float CrouchingSpeedUpCoefficient => _crouchingSpeedUpCoefficient;
        public float MinVelocityToSprint => _minVelocityToSprint;
    }
}