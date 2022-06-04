using Pool;
using UnityEngine;

namespace Utility
{
    public class Sparks : Missile
    {
        [SerializeField] private float lifeTime;
        private void OnEnable()
        {
            Invoke(nameof(OnDisableBehaviour), lifeTime);
        }

        private void OnDisableBehaviour()
        {
            Disable?.Invoke(this);
        }
    }
}