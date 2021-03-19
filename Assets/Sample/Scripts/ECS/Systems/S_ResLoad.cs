using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_ResLoad : BaseSystem, IFilteredFrameUpdatingSystem
    {

        public void FrameUpdate(Selection selection, float deltaTime)
        {
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
        }

        public Filter CreateFilter()
        {
            return new Filter<C_Asset>();
        }
    }
}
