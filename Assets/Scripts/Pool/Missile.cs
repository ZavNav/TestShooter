using UnityEngine;

namespace Pool
{
    public delegate void OnDisableDelegate(Missile projectile);
    public abstract class Missile : MonoBehaviour
    {
        public OnDisableDelegate Disable;
    }
}