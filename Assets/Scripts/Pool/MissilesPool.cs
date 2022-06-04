using System;
using Character;
using UnityEngine;
using UnityEngine.Pool;

namespace Pool
{
    public class MissilesPool : MonoBehaviour
    {
        private Missile _poolObject;
        private ObjectPool<Missile> _pool;
        private Transform _showPoint;
        private static Transform _poolParent;

        public void InitiatePool(Missile obj, Transform showPoint, int minProjectileAmount, int maxProjectileAmount)
        {
            if(_poolParent == null) _poolParent = GameObject.Find("MissilesPool").transform;
            
            _showPoint = showPoint;
            _poolObject = obj;
            _pool = 
                new ObjectPool<Missile>(OnCreateMissile, OnGettingFromPool,  
                    OnReturnToPool, OnDestroyMissile, false, 
                    minProjectileAmount, maxProjectileAmount);
        }
        
        
        private Missile OnCreateMissile()
        {
            var instance = Instantiate(_poolObject, Vector3.zero, Quaternion.identity, _poolParent);
            instance.Disable += ReturnMissileToPool;
            instance.gameObject.SetActive(false);
            return instance;
        }
        private void OnGettingFromPool(Missile obj)
        {
            obj.transform.position = _showPoint.position;
            obj.transform.rotation = _showPoint.rotation;
            obj.gameObject.SetActive(true);
        }
        private void OnReturnToPool(Missile obj) => obj.gameObject.SetActive(false);
        private void OnDestroyMissile(Missile obj) => Destroy(obj.gameObject);
        
        
        private void ReturnMissileToPool(Missile obj) => _pool.Release(obj);

        public void GetObjectFromPool()
        {
            _pool.Get();
        }

        public void GetObjectFromPool(Vector3 point, Vector3 normal)
        {
            _showPoint.position = point;
            _showPoint.forward = normal;
            _pool.Get();
        }
    }
}