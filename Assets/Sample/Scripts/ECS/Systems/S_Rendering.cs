using AillieoUtils;
using RockyECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class S_Rendering : BaseSystem, IFilteredFrameUpdatingSystem
    {
        private Handle<int> handle;
        private List<Action> renderActions = new List<Action>();

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

        public void FrameUpdate(Selection selection, float deltaTime)
        {
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
        }

        public Filter CreateFilter()
        {
            return new Filter<C_Renderer>() & new Filter<C_Mesh>() & new Filter<C_Material>();
        }
    }
}
