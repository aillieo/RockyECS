using System;
using System.Collections.Generic;
using System.Linq;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_GizmosDrawer : BaseSystem, IFilteredUpdatingSystem
    {
        private Handle eventHandle;
        private List<Action> gizmosActions = new List<Action>();
        private bool isNewFrame = false;

        public S_GizmosDrawer()
        {
            eventHandle = GizmosDrawer.Instance.gizmosEvent.AddListener(DrawGizmos);
        }

        public Filter[] CreateFilters()
        {
            return new Filter[]
            {
                new Filter<C_FrameIndex>(),
                new Filter<C_GizmosDrawer>()
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
                    break;
            }
        }

        private void DrawGizmos()
        {
            gizmosActions.ForEach(a => a?.Invoke());
        }
    }
}
