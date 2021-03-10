using AillieoUtils;
using RockyECS;
using UnityEngine;

namespace Sample
{
    public class S_GameObject : BaseSystem, IFilteredFrameUpdatingSystem
    {

        public void FrameUpdate(Selection selection, float deltaTime)
        {
            foreach (var s in selection)
            {
                C_GameObject c = s.GetComp<C_GameObject>();
                if (c.gameObject == null)
                {
                    C_Asset asset = s.GetComp<C_Asset>();
                    if (!string.IsNullOrWhiteSpace(asset.mesh))
                    {
                        var prefab = ResourceManager.Instance.LoadAsset<GameObject>(asset.mesh);
                        c.gameObject = GameObjectPool.Get(prefab);
                    }
                }

                if (c.gameObject != null)
                {
                    c.gameObject.transform.position = s.GetPosition().ToVec3();
                    c.gameObject.transform.rotation = Quaternion.Euler(Vector3.up * s.GetRotation());
                }

            }
        }

        public Filter CreateFilter()
        {
            return new Filter<C_GameObject>() & new Filter<C_Asset>();
        }
    }
}
