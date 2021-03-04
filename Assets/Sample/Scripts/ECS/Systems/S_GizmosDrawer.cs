using System;
using System.Collections.Generic;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_GizmosDrawer : BaseSystem, IFilteredFrameUpdatingSystem
    {
        private Handle eventHandle;
        private List<Action> gizmosActions = new List<Action>();

        public S_GizmosDrawer()
        {
            eventHandle = GizmosDrawer.Instance.gizmosEvent.AddListener(DrawGizmos);
        }

        public Filter CreateFilter()
        {
            return new Filter<C_GizmosDrawer>();
        }

        public void FrameUpdate(Selection selection, float deltaTime)
        {
            gizmosActions.Clear();

            foreach (var s in selection)
            {
                C_GizmosDrawer c = s.GetComp<C_GizmosDrawer>();
                Color color = c.color;
                Vector3 center = s.GetPosition().ToVec3();
                float r = c.size;
                gizmosActions.Add(() =>
                {
                    Gizmos.color = color;
                    Gizmos.DrawSphere(center, r);
                });
            }
        }

        private void DrawGizmos()
        {
            gizmosActions.ForEach(a => a?.Invoke());
        }
    }
}
