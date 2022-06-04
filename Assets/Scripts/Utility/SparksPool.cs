using Pool;
using UnityEngine;

namespace Utility
{
    public sealed class SparksPool : MonoBehaviour
    {
        [SerializeField] private Sparks sparks;
        
        private MissilesPool _pool;
        [SerializeField] private int minSystemsAmount = 5;
        [SerializeField] private int maxSystemsAmount = 10;

        
        private void Awake()
        {
            _pool = gameObject.AddComponent<MissilesPool>();
            _pool.InitiatePool(sparks, transform, minSystemsAmount, maxSystemsAmount);
        }

        public MissilesPool GetParticlesPool()
        {
            return _pool;
        }
    }
}