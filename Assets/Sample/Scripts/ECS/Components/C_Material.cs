using RockyECS;
using UnityEngine;

namespace Sample
{
    public class C_Material : IComponent
    {
        public Material material;
        public void Reset()
        {
            if(material != null)
            {

            }
            material = null;
        }
    }
}
