using AillieoUtils;
using RockyECS;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sample
{
    public class S_Rendering : BaseSystem, IFilteredUpdatingSystem
    {
        private Handle<int> handle;
        private List<Action> renderActions = new List<Action>();
        private bool isNewFrame = false;

        public S_Rendering()
        {
            handle = GameRenderer.Instance.renderEvent.AddListener(OnRender);
        }

        private void OnRender(int order)
        {
            foreach (var a in renderActions)
            {
                a?.Invoke();
            }
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

                    renderActions.Clear();

                    foreach (var e in selection)
                    {
                        C_Mesh mesh = e.GetComp<C_Mesh>();
                        C_Material material = e.GetComp<C_Material>();
                        C_Renderer r = e.GetComp<C_Renderer>();
                        Matrix4x4 mat = MapUtils.GetMatrix4x4(e);
                        renderActions.Add(() => {
                            Graphics.DrawMesh(mesh.mesh, mat, material.material, r.priority);
                        });
                    }

                    break;
            }

        }

        public Filter[] CreateFilters()
        {
            return new[]
            {
                new Filter<C_FrameIndex>(),
                new Filter<C_Renderer>() & new Filter<C_Mesh>() & new Filter<C_Material>()
            };
        }
    }
}
