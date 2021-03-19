using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_ClickHandler : BaseSystem, IFilteredFrameUpdatingSystem
    {
        public Filter CreateFilter()
        {
            return new Filter<C_ClickEvent>() & new Filter<C_ClickToBuild>();
        }

        public void FrameUpdate(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                C_ClickEvent c = s.GetComp<C_ClickEvent>();
                if (c == null)
                {
                    continue;
                }

                BuildTower(s, selection.context);
                s.RemoveComp(c);
            }
        }

        private void BuildTower(Entity entity, Context context)
        {
            Vector2 position = entity.GetPosition();

            Entity tower = context.CreateTower(CfgProxy.Instance.Get<TowerEntry>(1000));

            context.Remove(entity.id);
            tower.SetPosition(position);
        }
    }

}
