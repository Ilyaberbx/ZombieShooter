using System;
using UnityEngine;

namespace FPS
{
    public class Gravity : InGameBehaviour
    {
        public bool IsGrounded => TryCatchGround();

        public Vector3 Velocity
        {
            get => _velocity;
            set
            {
                if (value == null) return;
                _velocity = value;
            }
        }
        public float GeneralGravity => _gravity;
        public float GroundedGravity => _groundedGravity;

        private readonly float _groundedGravity = -2f;

        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private Transform _targetCheckGroundPosition;
        [SerializeField] private float _checkingGroundfRadius;
        [SerializeField] private Settings _settings;

        private CharacterController _characterController;
        private Vector3 _velocity;

        private void Awake() => Inititalize();

        private void OnDestroy() => GameStateController.OnGameStateChanged -= OnGameStateChanged;

        private void Update() => CalculateGravity();

        private void Inititalize()
        {
            GameStateController.OnGameStateChanged += OnGameStateChanged;
            _characterController = GetComponent<CharacterController>();
            _gravity *= _settings.GravityCoefficient;
        }
        public bool TryCatchGround()
        {
            Collider[] hits = Physics.OverlapSphere(_targetCheckGroundPosition.position, _checkingGroundfRadius);

            foreach (var check in hits)
            {
                if (check.transform != null)
                    if (check.transform.GetComponentInParent<Ground>() != null) return true;
            }

            return false;
        }

        private void CalculateGravity()
        {
            if (TryCatchGround() && Velocity.y < 0) _velocity.y = _groundedGravity;

            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(Velocity * Time.deltaTime);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_targetCheckGroundPosition.position, _checkingGroundfRadius);
            Gizmos.color = Color.white;
        }
    }

}