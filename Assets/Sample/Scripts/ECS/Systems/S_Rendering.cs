using AillieoUtils;
using RockyECS;
using System;
using UnityEngine;

namespace Sample
{
    public class S_Rendering : BaseSystem, IFilteredFrameUpdatingSystem
    {
        public S_Rendering()
        {
            GameRenderer.Instance.renderEvent.AddListener(OnRender);
        }

        private void OnRender(int order)
        {

        }

        public void FrameUpdate(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
            }
        }

        public Filter CreateFilter()
        {
            return new Filter<C_Renderer>() & new Filter<C_Mesh>() & new Filter<C_Material>();
        }
    }
}
