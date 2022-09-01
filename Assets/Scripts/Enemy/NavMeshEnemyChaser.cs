using UnityEngine;
using UnityEngine.AI;

namespace FPS
{
    public class NavMeshEnemyChaser : InGameBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _chasingSpeed;

        private Player _player;      
        private NavMeshAgent _navMeshAgent;

        public void Initialize(Player player)
        {
            this._player = player;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _chasingSpeed;
        }
        public void SetMoveable(bool isMoveable) => _navMeshAgent.speed = isMoveable ? _chasingSpeed : 0;
        private void Update() => Chase();
        private void Chase() => _navMeshAgent.SetDestination(_player.transform.position);

    }

}
