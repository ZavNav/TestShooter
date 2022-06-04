using System;
using Character;
using UnityEngine;

namespace Enemy
{
    public class AgentsWeapon : Weapon
    {
        [SerializeField] private float shotFrequency = 2;
        private const float ShootDelayTime = 1;

        protected override void Awake()
        {
            MuzzleTransform = transform;
            base.Awake();
        }

        private void Start()
        {
            InvokeRepeating(nameof(Shoot), ShootDelayTime, shotFrequency);
        }
        
        protected override void Shoot()
        {
            if(Physics.Raycast(transform.position, transform.forward, Mathf.Infinity, 3)) return;
            Pool.GetObjectFromPool();
        }
    }
}