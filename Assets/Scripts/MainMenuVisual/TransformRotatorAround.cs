using UnityEngine;

public class TransformRotatorAround : MonoBehaviour
{
    [SerializeField] private Transform _centerTransform;
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _speed;

    private void Update() => transform.RotateAround(_centerTransform.position, _axis, _speed * Time.deltaTime);

}
