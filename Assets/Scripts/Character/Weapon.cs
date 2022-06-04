using Pool;
using UnityEngine;

namespace Character
{
    public abstract class Weapon : MonoBehaviour
    {
        protected Transform MuzzleTransform;
        [SerializeField] private Projectile projectile;

        protected MissilesPool Pool;
        [SerializeField] private int minProjectileAmount = 5;
        [SerializeField] private int maxProjectileAmount = 10;
        protected virtual void Awake()
        {
            Pool = gameObject.AddComponent<MissilesPool>();
            Pool.InitiatePool(projectile, MuzzleTransform, minProjectileAmount, maxProjectileAmount);
        }

        protected abstract void Shoot();
    }
}