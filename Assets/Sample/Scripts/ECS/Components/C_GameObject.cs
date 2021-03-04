using RockyECS;
using UnityEngine;

namespace Sample
{
    public class C_GameObject: IComponent
    {
        public GameObject gameObject;

        public void Reset()
        {
            if (gameObject != null)
            {
                AillieoUtils.GameObjectPool.Recycle(gameObject);
                gameObject = null;
            }
        }
    }
}
