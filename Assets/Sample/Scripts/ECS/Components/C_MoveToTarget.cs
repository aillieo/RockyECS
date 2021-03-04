using RockyECS;
using UnityEngine;

namespace Sample
{
    public class C_MoveToTarget : IComponent
    {
        public float speed = 8f;
        public Vector2 targetPos;
        public int target;
        public void Reset()
        {
        }
    }

}
