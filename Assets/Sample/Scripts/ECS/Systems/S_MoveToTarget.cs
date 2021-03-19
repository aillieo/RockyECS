using UnityEngine;
using RockyECS;

namespace Sample
{
    public class S_MoveToTarget : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_MoveToTarget>();
        }

        public void Update(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                Update(s, deltaTime);
            }
        }

        public void Update(Entity entity, float deltaTime)
        {
            C_MoveToTarget c = entity.GetComp<C_MoveToTarget>();
            if (MapUtils.CloseEnough(entity.GetPosition(), c.targetPos))
            {
                return;
            }

            Vector2 pos = entity.GetPosition();
            Vector2 dir = c.targetPos - pos;
            dir.Normalize();
            pos += deltaTime * c.speed * dir;
            entity.SetRotation(VectorExt.ToRotation(dir));
            entity.SetPosition(pos);
        }
    }
}
