using UnityEngine;

namespace Utility
{
    public interface ITarget
    {
        void OnHit(Vector3 direction);
    }
}