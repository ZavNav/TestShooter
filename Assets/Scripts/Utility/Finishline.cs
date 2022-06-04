using System;
using Character;
using Controllers;
using UnityEngine;

namespace Utility
{
    public class Finishline : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterControl character))
            {
                StateController.Instance.CrossTheFinishLine();
            }
        }
    }
}