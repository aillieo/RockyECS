using System;
using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_TowerFindTargetGizmos : BaseSystem, IFilteredUpdatingSystem
    {
        private Handle eventHandle;
        private List<Action> gizmosActions = new List<Action>();
        private bool isNewFrame = false;

        public S_TowerFindTargetGizmos()
        {
            eventHandle = GizmosDrawer.Instance.gizmosEvent.AddListener(DrawLine);
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

                    gizmosActions.Clear();

                    foreach (var s in selection)
                    {
                        Entity target = s.GetComp<C_Target>().TargetAsEntity(selection.context);
                        if (!Entity.IsNullOrInvalid(target))
                        {
                            Vector3 p0 = s.GetPosition().ToVec3();
                            Vector3 p1 = target.GetPosition().ToVec3();
                            gizmosActions.Add(() => Gizmos.DrawLine(p0, p1));
                        }
                    }
                    break;
            }
        }

        public Filter[] CreateFilters()
        {
            return new Filter[]
            {
                new Filter<C_FrameIndex>(),
                new Filter<C_TowerFindTargetGizmos>() & new Filter<C_Target>()
            };
        }

        private void DrawLine()
        {
            Gizmos.color = Color.white;
            gizmosActions.ForEach(a => a?.Invoke());
        }
    }

}
