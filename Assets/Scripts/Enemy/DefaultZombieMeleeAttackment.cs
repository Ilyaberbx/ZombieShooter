
using System.Collections;
using UnityEngine;

namespace FPS
{
    [RequireComponent(typeof(DefaultZombie))]
    public class DefaultZombieMeleeAttackment : MonoBehaviour, IWeapon
    {

        [SerializeField] private int _damage;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _delayBeforePerformAttack;

        public int Damage => _damage;

        public void Attack() => StartCoroutine(AttackingRoutine());
        private IEnumerator AttackingRoutine()
        {
            yield return new WaitForSeconds(_delayBeforePerformAttack);

            Collider[] hits = Physics.OverlapSphere(_attackPoint.position, _attackRadius);

            foreach (var check in hits)
            {
                if (check.TryGetComponent(out IPlayer player))
                    player.UnitDamageHandler.ApplyDamage(Damage);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_attackPoint.transform.position, _attackRadius);
            Gizmos.color = Color.white;
        }
    }
}
