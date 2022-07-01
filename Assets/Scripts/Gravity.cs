using System;
using UnityEngine;

namespace FPS
{
    public class Gravity : GamePlayObjectMono
    {
        public bool IsGrounded => TryCatchGround();

        public Vector3 Velocity
        {
            get => _velocity;
            set
            {
                if (value != null)
                    _velocity = value;
            }
        }
        public float GeneralGravity => _gravity;
        public float GroundedGravity => _groundedGravity;

        [SerializeField] private Transform _targetCheckGroundPosition;
        [SerializeField] private float _checkingRayLength;
        [SerializeField] private Settings _settings;

        private float _gravity = -9.81f;
        private CharacterController _characterController;
        private Vector3 _velocity;
        private float _groundedGravity = -2f;

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
            RaycastHit hit;
            StrikeRay(out hit);
            if (hit.transform != null)
                if (hit.transform.GetComponent<Ground>() != null) return true;

            return false;
        }
        private void StrikeRay(out RaycastHit hit)
        {
            Ray ray = new Ray(_targetCheckGroundPosition.position, Vector3.down);
            Debug.DrawRay(_targetCheckGroundPosition.position, Vector3.down * _checkingRayLength, Color.red);
            Physics.Raycast(ray, out hit, _checkingRayLength);
        }
        private void CalculateGravity()
        {
            if (TryCatchGround() && Velocity.y < 0) _velocity.y = _groundedGravity;

            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(Velocity * Time.deltaTime);         
        }
    }

}