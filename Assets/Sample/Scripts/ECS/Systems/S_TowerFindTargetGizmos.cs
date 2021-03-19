using System;
using System.Collections.Generic;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_TowerFindTargetGizmos : BaseSystem, IFilteredFrameUpdatingSystem
    {
        private Handle eventHandle;
        private List<Action> gizmosActions = new List<Action>();

        public S_TowerFindTargetGizmos()
        {
            eventHandle = GizmosDrawer.Instance.gizmosEvent.AddListener(DrawLine);
        }

        public void FrameUpdate(Selection selection, float deltaTime)
        {
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
        }

        public Filter CreateFilter()
        {
            return new Filter<C_TowerFindTargetGizmos>() & new Filter<C_Target>();
        }

        private void DrawLine()
        {
            Gizmos.color = Color.white;
            gizmosActions.ForEach(a => a?.Invoke());
        }
    }

}
