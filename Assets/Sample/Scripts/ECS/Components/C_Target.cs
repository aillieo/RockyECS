using RockyECS;

namespace Sample
{
    public class C_Target : IComponent
    {
        public int target;

        public Entity GetEntity()
        {
            return Container.Instance.Get(target);
        }

        public void Reset()
        {
        }
    }
}
