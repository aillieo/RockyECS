using System.Linq;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_ClickHandler : BaseSystem, IFilteredUpdatingSystem
    {

        private bool isNewFrame = false;

        public Filter[] CreateFilters()
        {
            return new Filter[]
            {
                new Filter<C_FrameIndex>(),
                new Filter<C_ClickEvent>() & new Filter<C_ClickToBuild>()
            };
        }

        public void Update(int filterIndex, Selection selection, float deltaTime)
        {

            switch (filterIndex)
            {
                case 0:
                    isNewFrame = selection.First().GetComp<C_FrameIndex>().newFrame;
                    break;
                case 1:

                    if (!isNewFrame)
                    {
                        return;
                    }

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

                    break;
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
