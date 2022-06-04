using System;
using Controllers;
using UnityEngine;
using Utility;

namespace Enemy
{
    public class Agent : EnemyBehaviour, ITarget
    {
        public void OnHit(Vector3 direction)
        {
            Debug.Log("Agent defeated");
            StateController.Instance.RemoveEnemy(this);
        }
        private void OnMouseEnter()
        {
            ChangeColor(Color.yellow);
        }

        private void OnMouseExit()
        {
            ChangeColor(MyColor);
        }
    }
}