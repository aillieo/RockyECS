using RockyECS;
using UnityEngine;

namespace Sample
{
    public static class EntityExt
    {
        public static Vector2 GetPosition(this Entity entity)
        {
            return entity.GetComp<C_Position>().position;
        }

        public static void SetPosition(this Entity entity, Vector2 position)
        {
            entity.GetComp<C_Position>().position = position;
        }

        public static float GetRotation(this Entity entity)
        {
            return entity.GetComp<C_Rotation>().rotation;
        }

        public static void SetRotation(this Entity entity, float rotation)
        {
            entity.GetComp<C_Rotation>().rotation = rotation;
        }
    }
}