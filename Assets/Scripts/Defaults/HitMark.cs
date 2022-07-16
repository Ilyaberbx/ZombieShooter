using UnityEngine;

namespace FPS
{
    public class HitMark : MonoBehaviour
    {
        [SerializeField] private int _timeToDisactivate;

        private async void OnEnable()
        {
            await System.Threading.Tasks.Task.Delay(_timeToDisactivate * 100);
            gameObject.SetActive(false);
        }
    }
}
