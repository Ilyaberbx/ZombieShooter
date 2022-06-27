using UnityEngine;

namespace FPS
{
    [CreateAssetMenu]
    public class Settings : ScriptableObject
    {

        [Header("ViewSettings")]
        [Range(1, 200)] [SerializeField] private int _viewXSensetivity;
        [Range(1, 200)] [SerializeField] private int _viewYSensetivity;
        [SerializeField] private bool _viewInverted;

        [Header("GravitySettings")]
        [Range(1, 5)] [SerializeField] private float _gravityCoefficient;

        public int ViewInverted
        {
            get
            {
                if (_viewInverted)
                    return -1;

                return 1;
            }
        }
        public int ViewYSensetivity => _viewXSensetivity;
        public int ViewXSensetivity => _viewYSensetivity;
        public float GravityCoefficient => _gravityCoefficient;

    }
}
