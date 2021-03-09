using RockyECS;

namespace Sample
{
    public class C_Asset : IComponent
    {
        public string mesh;
        public string material;
        public void Reset()
        {
            mesh = null;
            material = null;
        }
    }
}
