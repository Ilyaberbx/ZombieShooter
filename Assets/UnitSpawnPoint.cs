using UnityEngine;

public class UnitSpawnPoint : MonoBehaviour
{
    [SerializeField] private float _radiusOfDisplay;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _radiusOfDisplay);
        Gizmos.color = Color.white;
    }
}
