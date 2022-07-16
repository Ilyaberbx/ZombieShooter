using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        public Transform Container { get; }
        private T _prefab;       
        private List<T> _freeElements = new List<T>();
        private bool _autoExpand;
        private List<T> _pool;

        public PoolMono(T prefab, int countInPool, bool autoExpand, Transform container = null)
        {
            this._prefab = prefab;
            this.Container = container;
            this._autoExpand = autoExpand;
            CreatePool(countInPool);
        }     
        public bool HasFreeElement(out T element)
        {
            foreach (var mono in _pool)
                if (!mono.gameObject.activeInHierarchy)
                    _freeElements.Add(mono);

            if (_freeElements.Count == 0)
            {
                element = null;
                return false;
            }
            element = _freeElements[Random.Range(0, _freeElements.Count)];
            element.gameObject.SetActive(true);
            _freeElements.Clear();
            return true;

        }
        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;

            if (_autoExpand)
                CreateObject(true);

            throw new System.Exception($"There is no elemnts in pool of type {typeof(T)}");
        }

        private void CreatePool(int countInPool)
        {
            this._pool = new List<T>();

            for (int i = 0; i < countInPool; i++)           
                CreateObject();
        }
        private void CreateObject(bool IsActiveByDefault = false)
        {
            var createdObj = Object.Instantiate(this._prefab, this.Container);
            createdObj.gameObject.SetActive(IsActiveByDefault);
            this._pool.Add(createdObj);
        }     
    }
}
