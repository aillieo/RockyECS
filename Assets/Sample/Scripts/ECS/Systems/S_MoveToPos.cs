using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_MoveToPos : BaseSystem, IFilteredUpdatingSystem
    {
        public Filter[] CreateFilters()
        {
            return new[]
            {
                new Filter<C_MoveToPos>() & new Filter<C_TargetPos>() & ( ~new Filter<C_MoveEnd>())
            };
        }

        public void Update(int filterIndex, Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                Update(s, deltaTime);
            }
        }

        private void Update(Entity entity, float deltaTime)
        {
            C_MoveToPos c = entity.GetComp<C_MoveToPos>();

            Vector2 targetPos = entity.GetComp<C_TargetPos>().targetPos;
            float move = deltaTime * c.speed;
            if (MapUtils.CloseEnough(entity.GetPosition(), targetPos, move))
            {
                entity.SetPosition(targetPos);
                entity.AddComp<C_MoveEnd>();
                return;
            }

            Vector2 pos = entity.GetPosition();
            Vector2 dir = targetPos - pos;
            dir.Normalize();
            pos += move * dir;
            entity.SetRotation(VectorExt.ToRotation(dir));
            entity.SetPosition(pos);
        }
    }

}
