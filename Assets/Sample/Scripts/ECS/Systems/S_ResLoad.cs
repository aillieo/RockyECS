using System.Linq;
using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_ResLoad : BaseSystem, IFilteredUpdatingSystem
    {

        private bool isNewFrame = false;

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
                        C_Asset asset = s.GetComp<C_Asset>();
                        if (!string.IsNullOrWhiteSpace(asset.mesh))
                        {
                            var mesh = ResourceManager.Instance.LoadAsset<Mesh>(asset.mesh);
                            s.AddComp<C_Mesh>().mesh = mesh;
                            asset.mesh = null;
                        }

                        if (!string.IsNullOrWhiteSpace(asset.material))
                        {
                            var material = ResourceManager.Instance.LoadAsset<Material>(asset.material);
                            s.AddComp<C_Material>().material = material;
                            asset.material = null;
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
                new Filter<C_Asset>()
            };
        }
    }
}
