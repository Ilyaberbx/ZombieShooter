using UnityEngine;

namespace FPS
{
    public abstract class BaseDecal : MonoBehaviour
    {
        [SerializeField] protected int _timeToDisactivate;
        private void OnEnable() => DecalLogic();
        private async void DecalLogic()
        {
            await System.Threading.Tasks.Task.Delay(_timeToDisactivate * 100);

            if (gameObject != null) 
            gameObject.SetActive(false);
        }

    }
}
