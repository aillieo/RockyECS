using Sample;
using UnityEngine;

public static class VectorExt
{
    public static Vector2 ToVec2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.z);
    }

    public static Vector3 ToVec3(this Vector2 vector2)
    {
        return ToVec3(vector2, GameConst.mapObjHeight);
    }
    
    public static Vector3 ToVec3(this Vector2 vector2, float y)
    {
        return new Vector3(vector2.x, y, vector2.y);
    }

    public static float ToRotation(this Vector2 direction)
    {
        return Vector2.SignedAngle(direction, Vector2.up);
    }
}
