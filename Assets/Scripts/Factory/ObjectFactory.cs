using UnityEngine;
using Zenject;

namespace FPS
{
    public abstract class ObjectFactory<T> where T : Component
    {
        protected DiContainer _diContainer;
        public void Init(T prefab) => _prefab = prefab;

        public ObjectFactory(DiContainer diContainer)
        {
            this._diContainer = diContainer;          
        }

        public virtual T Create(Vector3 position, Transform parent)
        {
            var defaultZombie = _diContainer.InstantiatePrefab(_prefab.gameObject, position, Quaternion.identity, parent);

            return defaultZombie.GetComponent<T>();
        }

        protected T _prefab;

    }
}