using System;
using Pool;
using UnityEngine;

namespace Character
{
    public class CharacterWeapon : Weapon
    {
        private const float ShootDelay = 0.5f;
        private float _nextShootTime;
        protected override void Awake()
        {
            MuzzleTransform = transform.GetChild(0);
            base.Awake();
        }

        private void Update()
        {
            Shoot();
        }

        protected override void Shoot()
        {
            if(!Input.GetMouseButtonDown(0) || _nextShootTime > Time.time) return;

            Debug.Log("Player shot");
            _nextShootTime = Time.time + ShootDelay;
            Pool.GetObjectFromPool();
        }
    }
}