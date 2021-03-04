using RockyECS;
using UnityEngine;

namespace Sample
{
    public static class MapUtils
    {
        public static bool CloseEnough(Vector2 lh, Vector2 rh, float? threshold = null)
        {
            if(threshold == null)
            {
                threshold = 0.001f;
            }
            return (lh - rh).sqrMagnitude < threshold;
        }
    
        public static bool CloseEnough(Entity lh, Entity rh, float? threshold = null)
        {
            return CloseEnough(lh.GetPosition(), rh.GetPosition(), threshold);
        }

        public static Matrix4x4 GetMatrix4x4(this Entity e)
        {
            return Matrix4x4.TRS(e.GetPosition().ToVec3(),Quaternion.Euler(0, e.GetRotation(), 0), Vector3.one);
        }
    }
}
