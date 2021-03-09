using RockyECS;
using UnityEngine;

namespace Sample
{
    public class C_Mesh : IComponent
    {
        public Mesh mesh;
        public void Reset()
        {
            if(mesh != null)
            {
            }
            mesh = null;
        }
    }
}
