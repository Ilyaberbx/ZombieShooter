using UnityEngine;
using Zenject;

namespace FPS
{
    public abstract class EnemyFactory<T> : GamePlayBehaviour where T : Component
    {
        private DiContainer _diContainer;

        [Inject]
        public void Constructor(DiContainer diContainer) => this._diContainer = diContainer;

        public virtual IEnemy CreateEnemy(Vector3 position, Transform parent)
        {
      
            if (GameStateController.CurrentState != GameState.GamePlay) return null;

            var defaultZombie = _diContainer.InstantiatePrefab(_prefab.gameObject, position, Quaternion.identity, parent);

            return defaultZombie.GetComponent<IEnemy>();
        }
                
        [SerializeField] private T _prefab;
     
    }
}
