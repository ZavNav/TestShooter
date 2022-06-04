using System;
using Pool;
using UnityEngine;
using Utility;

namespace Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : Missile
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private float projectileSpeed;
        private Rigidbody _projectileRb;

        private static SparksPool _sparksPool;
        
        private void Awake()
        {
            _projectileRb = GetComponent<Rigidbody>();
            if (_sparksPool == null) _sparksPool = FindObjectOfType<SparksPool>();
        }

        private void OnEnable()
        {
            _projectileRb.velocity = Vector3.zero;
            _projectileRb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
            Invoke(nameof(OnDisableBehaviour), lifeTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            collision.gameObject.GetComponent<ITarget>()?.OnHit(_projectileRb.velocity);
            
            _sparksPool.GetParticlesPool().GetObjectFromPool(transform.position, collision.contacts[0].normal);
            
            CancelInvoke();
            Disable?.Invoke(this);
        }

        private void OnDisableBehaviour()
        {
            Disable?.Invoke(this);
        }
    }
}